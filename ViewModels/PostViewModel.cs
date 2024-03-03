using Instagram.Commands;
using Instagram.Databases;
using Instagram.Enums;
using Instagram.Interfaces;
using Instagram.JSONModels;
using Instagram.Models;
using Instagram.Repositories;
using Instagram.Services;
using Instagram.StartupHelpers;
using Instagram.Validations;
using Instagram.Views;
using System;
using System.Collections;
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
    public class PostViewModel : ViewModelBase
    {
        #region Resources
        private readonly string _path;
        public BitmapImage UserProfilePhotoSource { get; set; }
        public BitmapImage ProfilePhotoSource { get; set; }
        public BitmapImage PostPhotoSource { get; set; }
        public string LikeIconPath { get; set; }
        public string CommentIconPath { get; set; }
        public string MessageIconPath { get; set; }
        public string TrashIconPath { get; set; }
        public string ShowMoreIconPath { get; set; }
        public string ShowLessIconPath { get; set; }
        #endregion
        #region OnPropertyChangeProperties
        private bool _IsCommentClickedToCreate = false;
        public bool IsCommentClickedToCreate 
        { 
            get { return _IsCommentClickedToCreate; }
            set
            {
                _IsCommentClickedToCreate = value;
                OnPropertyChanged(nameof(IsCommentClickedToCreate));
            }
        }
        private bool _IsPostYour;
        public bool IsPostYour 
        { 
            get { return _IsPostYour; }
            set
            {
                _IsPostYour = value;
                OnPropertyChanged(nameof(IsPostYour));
            }
        }
        private string _CommentContent;
        public string CommentContent
        {
            get { return _CommentContent; }
            set
            {
                _CommentContent = value;
                OnPropertyChanged(nameof(CommentContent));
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
        private string _Description;
        public string Description
        {
            get { return _Description; }
            set
            {
                _Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        private string _CommentsNumber;
        public string CommentsNumber 
        {
            get { return _CommentsNumber; }
            set
            {
                _CommentsNumber = value;
                OnPropertyChanged(nameof(CommentsNumber));
            }
        }
        private bool _ShowMoreComments = false;
        public bool ShowMoreComments 
        { 
            get { return _ShowMoreComments; }
            set 
            {
                _ShowMoreComments = value;
                OnPropertyChanged(nameof(ShowMoreComments));
            }
        }
        private bool _IsPostLiked;
        public bool IsPostLiked 
        {
            get { return _IsPostLiked; } 
            set
            {
                _IsPostLiked = value;
                OnPropertyChanged(nameof(IsPostLiked));
            }
        }
        private string _Tags;
        public string Tags
        {
            get
            {
                return _Tags;
            }
            set
            {
                _Tags = value;
                OnPropertyChanged(nameof(Tags));
            }
        }
        private ObservableCollection<CommentView> _CommentsSection = new ObservableCollection<CommentView>();
        public ObservableCollection<CommentView> CommentsSection
        {
            get 
            {
                return _CommentsSection;
            }
            set
            {
                _CommentsSection = value;
                OnPropertyChanged(nameof(CommentsSection));
            } 
        }
        #endregion
        #region Commands
        public ICommand MoreComments { get; set; }
        public ICommand LessComments { get; set; }
        public ICommand LikeButton { get; set; }
        public ICommand CommentButton { get; set; }
        public ICommand MessageButton { get; set; }
        public ICommand CreateComment { get; set; }
        public ICommand DeletePost { get; set; }
        #endregion
        #region NormalProperties
        public string ProfileName { get; set; }
        public string Location { get; set; }
        #endregion
        #region PrivateProperties
        private Post _post;
        private readonly Action _ChangeHomeTheme;
        private readonly IAbstractFactory<CommentView> _commentFactory;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserLikedRepository _userLikedRepository;
        private readonly IBothCommentsRepository<Comment> _commentRepository;
        private readonly int _postId;
        private int _userId;
        #endregion
        public PostViewModel(InstagramDbContext db, IAbstractFactory<CommentView> commentFactory, int postId, Action ChangeHomeTheme)
        {
            #region PrivatePropertiesAssignment
            _postId = postId;
            _commentFactory = commentFactory;
            _postRepository = new PostRepository(db);
            _userLikedRepository = new UserLikedRepository(db);
            _commentRepository = new BothCommentsRepository<Comment>(db);
            _userRepository = new UserRepository(db);
            _path = ConfigurationManager.AppSettings.Get("ResourcesPath");
            _ChangeHomeTheme = ChangeHomeTheme;
            #endregion
            GetPost();
            GeneratePostData();
            GenerateComments();
            #region CommandsInstances
            MoreComments = new ShowMoreLessCommentsCommand(ShowMoreLessCommentsChange);
            LessComments = new ShowMoreLessCommentsCommand(ShowMoreLessCommentsChange);
            LikeButton = new LikeCommand(LikedThingsEnum.Post, _userId, _postId, UpdateLikes, _userLikedRepository);
            CommentButton = new CommentButtonCommand(UpdateIsCommentClickedToCreateValue);
            CreateComment = new CommentCreateCommand(CreateNewCommentAsync);
            MessageButton = new OpenCommunicatorWindowCommand();
            DeletePost = new DeletePostCommand(_postId, _postRepository);
            #endregion
            InitResources();
        }

        private void InitResources()
        {
            LikeIconPath = $"{_path}likeIcon.png";
            CommentIconPath = $"{_path}commentIcon.png";
            MessageIconPath = $"{_path}messageIcon.png";
            TrashIconPath = $"{_path}trashIcon.png";
            ShowMoreIconPath = $"{_path}showMoreIcon.png";
            ShowLessIconPath = $"{_path}showLessIcon.png";
        }

        private async Task GeneratePostData()
        {
            _userId = await GetUser.IdFromFile();
            User user = await _userRepository.GetUserWithPhotoAndRequestsAsync(_userId);
            PostPhotoSource = ConvertImage.FromByteArray(_post.Image.ImageBytes);
            ProfilePhotoSource = ConvertImage.FromByteArray(_post.User.ProfilePhoto.ImageBytes);
            UserProfilePhotoSource = ConvertImage.FromByteArray(user.ProfilePhoto.ImageBytes);
            ProfileName = _post.User.Nickname;
            Location = _post.Location;
            Description = _post.Description;
            Tags = ConvertTagsToString.Convert(_post.Tags);
            UpdateCommentsNumber(_post.Comments.Count());
            UpdateLikesNumber(_post.Likes);
            IsPostLiked = await _userLikedRepository.IsLikedBy(_userId, LikedThingsEnum.Post, _postId);
            IsPostYour = _userId == _post.User.Id ? true : false;
        }

        private async Task GetPost()
        {
            _post = await _postRepository.GetPostWithAllDataAsync(_postId);
        }

        private void GenerateComments()
        {
            Action ChangeTheme = _ChangeHomeTheme;
            foreach (Comment comment in _post.Comments)
            {
                CommentView commentView = _commentFactory.Create();
                commentView.AddDataContext(comment.Id, ChangeTheme);
                CommentsSection.Add(commentView);
            }
        }

        public void UpdateLikes(bool likedOrRemoved)
        {
            if (likedOrRemoved)
            {
                IsPostLiked = true;
                _post.Likes += 1;
            }
            else
            {
                IsPostLiked = false;
                _post.Likes -= 1;
            }
            _postRepository.UpdatePostAsync(_post);
            UpdateLikesNumber(_post.Likes);
        }   

        public void ShowMoreLessCommentsChange()
        {
            ShowMoreComments ^= true;
            _ChangeHomeTheme.Invoke();
        }

        public void UpdateCommentsNumber(int commentsNumber)
        {
            CommentsNumber = $"{commentsNumber} COMMENTS";
        }

        public void UpdateLikesNumber(int newLikesNumber)
        {
            LikesNumber = $"{newLikesNumber} LIKES";
            _post.Likes = newLikesNumber;
        }

        public void UpdateIsCommentClickedToCreateValue()
        {
            IsCommentClickedToCreate ^= true;
        }

        public async Task CreateNewCommentAsync()
        {
            IsCommentClickedToCreate = false;
            Comment comment = new Comment()
            {
                AuthorId = _userId,
                PostId = _post.Id,
                Content = CommentContent,
                PublicationDate = DateTime.Now
            };
            await _commentRepository.AddCommentAsync(comment);
    }
    }
}
