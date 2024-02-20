using Instagram.Commands;
using Instagram.Databases;
using Instagram.JSONModels;
using Instagram.Models;
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
        private Comment _comment;
        private bool showMoreCommentResponses = true;
        private bool clicked = true;
        private int _userId;
        private bool _isDarkMode;
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
        public CommentViewModel(Comment comment, int userId)
        {
            #region PrivatePropertiesAssignment
            _comment = comment;
            _userId = userId;
            _path = ConfigurationManager.AppSettings.Get("ResourcesPath");
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
        public async Task LoadResponsesToCommentAsync()
        {
            CommentResponses = new ObservableCollection<ReplyCommentView>();
            if (showMoreCommentResponses && clicked)
            {
                ShowMoreLessButtonContent = showLessIconSource;
                var GetDataFromDatabaseAsync = async Task () =>
                {
                    //using (var db = new InstagramDbContext("MainDb"))
                    //{
                    //    foreach (CommentResponse cm in db.CommentResponses.Where(c => c.CommentId == _comment.Id).ToList())
                    //    {
                    //        CommentResponses.Add(new ReplyCommentView(cm, _userId));
                    //    }
                    //}
                };
                await GetDataFromDatabaseAsync.Invoke();
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
            var GetDataFromDatabaseAsync = async Task () =>
            {
                //using (var db = new InstagramDbContext("MainDb"))
                //{
                //    // do it easier
                //    User profile = db.Users.First(u => u.Id == _comment.AuthorId);
                //    CommentProfileName = profile.Nickname;
                //    ProfileImage profilePhoto = db.ProfileImages.First(p => p.UserId == profile.Id);
                //    CommentProfilePhotoSource = ConvertImage.FromByteArray(profilePhoto.ImageBytes);
                //    CommentText = _comment.Content;
                //    PublicationDate = _comment.PublicationDate;
                //    UpdateLikesNumber(_comment.Likes);
                //    ProfileImage userReplyCommentPhoto = db.ProfileImages.First(pi => pi.Id == _userId);
                //    ReplyProfilePhotoSource = ConvertImage.FromByteArray(userReplyCommentPhoto.ImageBytes);
                //    AreAnyComments = db.CommentResponses.Where(c => c.CommentId == _comment.Id).ToList().Count() > 0 ? true : false;
                //    ShowMoreLessButtonContent = showMoreIconSource;
                //    ChangeIsUserLiked(db.UsersLiked.Where(u => (
                //        u.UserThatLikedId == _userId &&
                //        u.LikedThingId == _comment.Id &&
                //        (int)u.LikedThing == (int)LikedThingsEnum.Comment
                //        )).ToList().Count());
                //}
            };
            await GetDataFromDatabaseAsync.Invoke();
        }
        public void UpdateLikesNumber(int likesNumber)
        {
            LikesNumber = $"{likesNumber} LIKES";
            _comment.Likes = likesNumber;
        }
        public void ChangeIsUserLiked(int isUserLikedCount)
        {
            if (isUserLikedCount == 0) 
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
            var CreateReplyAsync = async Task () =>
            {
                //using (var db = new InstagramDbContext("MainDb"))
                //{
                //    db.Comments.First(c => c.Id == _comment.Id).CommentResponses.Add(new CommentResponse()
                //    {
                //        AuthorId = _userId,
                //        Content = _ReplyCommentContent,
                //        Likes = 0,
                //        PublicationDate = DateTime.Now
                //    });
                //    db.SaveChanges();
                //    clicked = false;
                //    LoadResponsesToCommentAsync();
                //}
            };
            await CreateReplyAsync.Invoke();
            ChangeReplyClickedStatus();
        }
    }
}
