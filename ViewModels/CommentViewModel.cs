using Instagram.Commands;
using Instagram.Databases;
using Instagram.Enums;
using Instagram.Interfaces;
using Instagram.JSONModels;
using Instagram.Models;
using Instagram.Repositories;
using Instagram.Services;
using Instagram.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Instagram.ViewModels
{
    public class CommentViewModel : ViewModelBase
    {
        #region Resources
        private readonly string _path;
        public string LikeIconSource { get; set; }
        public string ReplyIconSource { get; set; }
        public string RemoveIconSource { get; set; }
        private string showMoreIconSource { get; set; }
        private string showLessIconSource { get; set; }
        public BitmapImage CommentProfilePhotoSource { get; set; }
        public BitmapImage ReplyProfilePhotoSource { get; set; }
        #endregion
        #region NormalProperties
        public string CommentProfileName { get; set; }
        public string CommentText { get; set; }
        public DateTime PublicationDate { get; set; }
        #endregion
        #region PrivateProperties
        private Comment _comment;
        private bool showMoreCommentResponses = true;
        private bool clicked = true;
        private int _userId;
        private bool _isDarkMode;
        private ICommentResponseRepository _commentResponseRepository;
        private IUserRepository _userRepository;
        private IUserLikedRepository _userLikedRepository;
        #endregion
        #region OnPropertyChangeProperties
        private bool _IsCommentLiked;
        public bool IsCommentLiked 
        {
            get { return _IsCommentLiked; }
            set
            {
                _IsCommentLiked = value;
                OnPropertyChanged(nameof(IsCommentLiked));
            }
        }
        private bool _AreAnyComments;
        public bool AreAnyComments 
        { 
            get { return _AreAnyComments; }
            set
            {
                _AreAnyComments = value;
                OnPropertyChanged(nameof(AreAnyComments));
            }
        }
        private string _ShowMoreLessButtonContent;
        public string ShowMoreLessButtonContent 
        { 
            get { return _ShowMoreLessButtonContent; }
            set
            {
                _ShowMoreLessButtonContent = value;
                OnPropertyChanged(nameof(ShowMoreLessButtonContent));
            }
        }
        private string _ReplyCommentContent;
        public string ReplyCommentContent
        {
            get { return _ReplyCommentContent; }
            set
            {
                _ReplyCommentContent = value;
                OnPropertyChanged(nameof(ReplyCommentContent));
            }
        }
        private string _LikesNumber;
        public string LikesNumber 
        {
            get { return _LikesNumber; } 
            set
            {
                _LikesNumber = value;
                OnPropertyChanged(nameof(LikesNumber));
            }
        }
        private bool _IsUserLiked;
        public bool IsUserLiked 
        {
            get { return _IsUserLiked; }
            set
            {
                _IsUserLiked = value;
                OnPropertyChanged(nameof(IsUserLiked));
            }
        }
        private bool _IsReplyClicked;
        public bool IsReplyClicked 
        {
            get { return _IsReplyClicked; }
            set
            {
                _IsReplyClicked = value;
                OnPropertyChanged(nameof(IsReplyClicked));
            } 
        }
        private ObservableCollection<ReplyCommentView> _CommentResponses;
        public ObservableCollection<ReplyCommentView> CommentResponses 
        { 
            get { return _CommentResponses; }
            set
            {
                _CommentResponses = value;
                OnPropertyChanged(nameof(CommentResponses));
            }
        }
        private bool _IsCommentYours;
        public bool IsCommentYours
        {
            get { return _IsCommentYours; }
            set
            {
                _IsCommentYours = value;
                OnPropertyChanged(nameof(IsCommentYours));
            }
        }
        #endregion
        #region Commands
        public ICommand LikeButton { get; set; }
        public ICommand ReplyButton { get; set; }
        public ICommand RemoveButton { get; set; }
        public ICommand CreateCommentReply { get; set; }
        public ICommand ShowMoreLessButton { get; set; }
        #endregion
        public CommentViewModel(Comment comment, int userId, InstagramDbContext db)
        {
            #region PrivatePropertiesAssignment
            _comment = comment;
            _userId = userId;
            _path = ConfigurationManager.AppSettings.Get("ResourcesPath");
            _commentResponseRepository = new CommentResponseRepository(db);
            _userRepository = new UserRepository(db);
            _userLikedRepository = new UserLikedRepository(db);
            #endregion
            IsCommentYours = comment.AuthorId == userId ? true : false;
            LoadDataFromDatabaseAsync();
            InitResources();
            #region CommandsInstances
            LikeButton = new LikeCommand(LikedThingsEnum.Comment, userId, comment.Id, UpdateLikesNumber, ChangeIsUserLiked);
            ReplyButton = new ReplyToCommentCommand(ChangeReplyClickedStatus);
            RemoveButton = new RemoveCommentCommand();
            CreateCommentReply = new CreateCommentReplyCommand(CreateNewReplyAsync);
            ShowMoreLessButton = new ShowMoreLessCommentsCommand(LoadResponsesToCommentAsync, ChangeTheme);
            #endregion
        }
        private void InitResources()
        {
            LikeIconSource = $"{_path}likeIcon.png";
            ReplyIconSource = $"{_path}replyIcon.png";
            RemoveIconSource = $"{_path}trashIcon.png";
            showMoreIconSource = $"{_path}showMoreIcon.png";
            showLessIconSource = $"{_path}showLessIcon.png";
        }
        private async Task GetDarkModeFromJsonFileAsync()
        {
            var json = new JSON<UserDataModel>("UserData");
            _isDarkMode = await json.GetDarkModeAsync();
        }
        public void ChangeTheme()
        {
            GetDarkModeFromJsonFileAsync();
            if (CommentResponses != null)
            {
                foreach (var comment in CommentResponses)
                {
                    comment.ChangeCommentTheme(_isDarkMode);
                }
            }
        }
        private async Task LoadResponsesToCommentAsync()
        {
            CommentResponses = new ObservableCollection<ReplyCommentView>();
            if (showMoreCommentResponses && clicked)
            {
                ShowMoreLessButtonContent = showLessIconSource;
                foreach (var commentResponse in _comment.CommentResponses)
                {
                    CommentResponses.Add(new ReplyCommentView(commentResponse, _userId));
                }
                showMoreCommentResponses = false;
            }
            else if(clicked)
            {
                showMoreCommentResponses = true;
                ShowMoreLessButtonContent = showMoreIconSource;
            }
            clicked = true;
        }
        private async void LoadDataFromDatabaseAsync()
        {
            User author = await _userRepository.GetUserWithPhotoAndRequestsAsync(_comment.AuthorId);
            CommentProfileName = author.Nickname;
            CommentProfilePhotoSource = ConvertImage.FromByteArray(author.ProfilePhoto.ImageBytes);
            CommentText = _comment.Content;
            PublicationDate = _comment.PublicationDate;
            UpdateLikesNumber(_comment.Likes);
            User user = await _userRepository.GetUserWithPhotoAndRequestsAsync(_userId);
            ReplyProfilePhotoSource = ConvertImage.FromByteArray(user.ProfilePhoto.ImageBytes); ;
            AreAnyComments = _comment.CommentResponses.Count() > 0 ? true : false;
            ShowMoreLessButtonContent = showMoreIconSource;
            ChangeIsUserLiked(_userLikedRepository.IsLikedBy(_userId, LikedThingsEnum.Comment, _comment.Id).Result);
        }
        public void UpdateLikesNumber(int likesNumber)
        {
            LikesNumber = $"{likesNumber} LIKES";
            _comment.Likes = likesNumber;
        }
        public void ChangeIsUserLiked(bool isUserLikedCount)
        {
            if (!isUserLikedCount) 
            {
                IsUserLiked = true;
            }
            else
            {
                IsUserLiked = false;
            }
        }
        public void ChangeReplyClickedStatus()
        {
            IsReplyClicked ^= true;
        }
        public async Task CreateNewReplyAsync()
        {
            CommentResponse commentResponse = new CommentResponse()
            {
                AuthorId = _userId,
                Content = _ReplyCommentContent,
                Likes = 0,
                PublicationDate = DateTime.Now
            };
            await _commentResponseRepository.AddCommentResponseAsync(commentResponse);
            LoadResponsesToCommentAsync();
            clicked = false;
            ChangeReplyClickedStatus();
        }
    }
}
