using Instagram.Commands;
using Instagram.Databases;
using Instagram.Enums;
using Instagram.Interfaces;
using Instagram.JSONModels;
using Instagram.Models;
using Instagram.Repositories;
using Instagram.Services;
using Instagram.StartupHelpers;
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
        public BitmapImage CommentProfilePhotoSource { get; set; }
        #endregion
        #region NormalProperties
        public string CommentProfileName { get; set; }
        public string CommentText { get; set; }
        public DateTime PublicationDate { get; set; }
        #endregion
        #region PrivateProperties
        private Comment _comment;
        private User _authorUser;
        private User _user;
        private readonly IBothCommentsRepository<CommentResponse> _commentResponseRepository;
        private readonly IBothCommentsRepository<Comment> _commentRepository;
        private readonly IUserLikedRepository _userLikedRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAbstractFactory<ReplyCommentView> _replyCommentFactory;
        private readonly Func<Task> _ChangeHomeTheme;
        private int _userId;
        private int _commentId;
        #endregion
        #region OnPropertyChangeProperties
        private BitmapImage _ReplyProfilePhotoSource;
        public BitmapImage ReplyProfilePhotoSource
        {
            get { return _ReplyProfilePhotoSource; }
            set
            {
                _ReplyProfilePhotoSource = value;
                OnPropertyChanged(nameof(ReplyProfilePhotoSource));
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
        private bool _AreCommentsShown;
        public bool AreCommentsShown
        {
            get { return _AreCommentsShown; }
            set
            {
                _AreCommentsShown = value;
                OnPropertyChanged(nameof(AreCommentsShown));
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
        private bool _IsCommentYour;
        public bool IsCommentYour
        {
            get { return _IsCommentYour; }
            set
            {
                _IsCommentYour = value;
                OnPropertyChanged(nameof(IsCommentYour));
            }
        }
        private ObservableCollection<ReplyCommentView> _CommentResponses = new ObservableCollection<ReplyCommentView>();
        public ObservableCollection<ReplyCommentView> CommentResponses 
        { 
            get { return _CommentResponses; }
            set
            {
                _CommentResponses = value;
                OnPropertyChanged(nameof(CommentResponses));
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
        public CommentViewModel(InstagramDbContext db, IAbstractFactory<ReplyCommentView> replyCommentFactory, int commentId, Func<Task> ChangeHomeTheme)
        {
            #region PrivatePropertiesAssignment
            _commentId = commentId;
            _path = ConfigurationManager.AppSettings.Get("ResourcesPath");
            _commentRepository= new BothCommentsRepository<Comment>(db);
            _commentResponseRepository = new BothCommentsRepository<CommentResponse>(db);
            _userLikedRepository = new UserLikedRepository(db);
            _userRepository = new UserRepository(db);
            _replyCommentFactory = replyCommentFactory;
            _ChangeHomeTheme = ChangeHomeTheme;
            #endregion
            GetCommentAndUserAsync();
            GenerateCommentDataAsync();
            InitResources();
            #region CommandsInstances
            LikeButton = new LikeCommand(LikedThingsEnum.Comment, _userId, _commentId, UpdateLikes, _userLikedRepository);
            ReplyButton = new ReplyToCommentCommand(ChangeReplyClickedStatus);
            RemoveButton = new RemoveCommentCommand();
            CreateCommentReply = new CreateCommentReplyCommand(CreateNewReplyAsync);
            ShowMoreLessButton = new ShowMoreLessCommentsCommand(ShowMoreLess);
            #endregion
        }

        private void InitResources()
        {
            LikeIconSource = $"{_path}likeIcon.png";
            ReplyIconSource = $"{_path}replyIcon.png";
            RemoveIconSource = $"{_path}trashIcon.png";
            ShowMoreLessButtonContent = $"{_path}showMoreIcon.png";
        }

        private async Task GetCommentAndUserAsync()
        {
            _comment = await _commentRepository.GetCommentWithResponsesAsync(_commentId);
            _authorUser = await _userRepository.GetUserWithPhotoAndRequestsAsync(_comment.AuthorId);
            _userId = await GetUser.IdFromFile();
            _user = await _userRepository.GetUserWithPhotoAndRequestsAsync(_userId);   
        }

        private async Task GenerateCommentDataAsync()
        {
            CommentProfilePhotoSource = ConvertImage.FromByteArray(_authorUser.ProfilePhoto.ImageBytes);
            CommentProfileName = _authorUser.Nickname;
            CommentText = _comment.Content;
            AreAnyComments = _comment.CommentResponses.Count() > 0 ? true : false;
            UpdateLikesNumber(_comment.Likes);
            IsCommentLiked = await _userLikedRepository.IsLikedBy(_userId, LikedThingsEnum.Comment, _commentId);
            IsCommentYour = _userId == _comment.AuthorId ? true : false;
            PublicationDate = _comment.PublicationDate;
            ReplyProfilePhotoSource = ConvertImage.FromByteArray(_user.ProfilePhoto.ImageBytes);
        }

        public void UpdateLikesNumber(int likesNumber)
        {
            LikesNumber = $"{likesNumber} LIKES";
            _comment.Likes = likesNumber;
        }

        public void UpdateLikes(bool likedOrRemoved)
        {
            if (likedOrRemoved)
            {
                IsCommentLiked = true;
                _comment.Likes += 1;
            }
            else
            {
                IsCommentLiked = false;
                _comment.Likes -= 1;
            }
            _commentRepository.UpdateCommentAsync(_comment);
            UpdateLikesNumber(_comment.Likes);
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
                CommentId = _comment.Id,
                Content = ReplyCommentContent,
                Likes = 0,
                PublicationDate = DateTime.Now
            };
            await _commentResponseRepository.AddCommentAsync(commentResponse);
            Comment comment = await _commentRepository.GetCommentWithResponsesAsync(_comment.Id);
            List<CommentResponse> commentResponses = comment.CommentResponses;
            AddCommentToResponse(commentResponses[commentResponses.Count() - 1]);
            ChangeReplyClickedStatus();
            ReplyCommentContent = "";
            AreAnyComments = true;
            await _ChangeHomeTheme.Invoke();
        }

        public async Task ShowMoreLess()
        {
            if (!AreCommentsShown)
            {
                ShowMoreLessButtonContent = $"{_path}showLessIcon.png";
                GenerateResponsesAsync();
                await _ChangeHomeTheme.Invoke();
            }
            else
            {
                ShowMoreLessButtonContent = $"{_path}showMoreIcon.png";
            }
            AreCommentsShown ^= true;
        }

        private void AddCommentToResponse(CommentResponse commentResponse)
        {
            ReplyCommentView replyComment = _replyCommentFactory.Create();
            replyComment.AddDataContext(commentResponse.Id);
            CommentResponses.Add(replyComment);
        }

        private async Task GenerateResponsesAsync() 
        {
            foreach (var commentResponse in _comment.CommentResponses)
            {
                AddCommentToResponse(commentResponse);
            }
        }
    }
}
