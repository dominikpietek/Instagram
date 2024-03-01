using Instagram.Commands;
using Instagram.Databases;
using Instagram.Enums;
using Instagram.Interfaces;
using Instagram.Models;
using Instagram.Repositories;
using Instagram.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace Instagram.ViewModels
{
    public class ReplyCommentViewModel : ViewModelBase
    {
        
        #region Resources
        private string _path;
        public string LikeIconPath { get; set; }
        public string TrashIconPath { get; set; }
        public BitmapImage CommentProfilePhotoSource { get; set; }
        #endregion
        #region NormalProperties
        public string CommentProfileName { get; set; }
        public string CommentText { get; set; }
        public DateTime PublicationDate { get; set; }
        #endregion
        #region OnPropertyChangeProperties
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
        private bool _IsReplyYours;
        public bool IsReplyYours 
        {
            get { return _IsReplyYours; }
            set
            {
                _IsReplyYours = value;
                OnPropertyChanged(nameof(IsReplyYours));
            }
        }
        #endregion
        #region PrivateProperties
        private readonly int _commentId;
        private CommentResponse _comment;
        private readonly IBothCommentsRepository<CommentResponse> _commentRepository;
        private readonly IUserLikedRepository _userLikedRepository;
        private readonly IUserRepository _userRepository;
        #endregion
        #region Commands
        public ICommand LikeButton { get; set; }
        public ICommand RemoveButton { get; set; }
        #endregion
        public ReplyCommentViewModel(InstagramDbContext db, int commentId)
        {
            #region PrivatePropertiesAssignment
            _commentId = commentId;
            _path = ConfigurationManager.AppSettings.Get("ResourcesPath");
            _commentRepository = new BothCommentsRepository<CommentResponse>(db);
            _userLikedRepository = new UserLikedRepository(db);
            _userRepository = new UserRepository(db);
            #endregion
            LoadDataFromDatabaseAsync();
            #region CommandInstances
            LikeButton = new LikeCommand(LikedThingsEnum.CommentResponse, _comment.AuthorId, _commentId, UpdateLikes, _userLikedRepository);
            RemoveButton = new RemoveCommentCommand();
            #endregion
            InitResources();
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

        private void InitResources()
        {
            LikeIconPath = $"{_path}likeIcon.png";
            TrashIconPath = $"{_path}trashIcon.png";
        }

        private async Task LoadDataFromDatabaseAsync()
        {
            _comment = await _commentRepository.GetCommentAsync(_commentId);
            User user = await _userRepository.GetUserWithPhotoAndRequestsAsync(_comment.AuthorId);
            int userId = await GetUser.IdFromFile();
            CommentProfilePhotoSource = ConvertImage.FromByteArray(user.ProfilePhoto.ImageBytes);
            UpdateLikesNumber(_comment.Likes);
            IsCommentLiked = await _userLikedRepository.IsLikedBy(userId, LikedThingsEnum.CommentResponse, _commentId);
            IsReplyYours = userId == _comment.AuthorId ? true : false;
            CommentText = _comment.Content;
            PublicationDate = _comment.PublicationDate;
        }

        public void UpdateLikesNumber(int likesNumber)
        {
            LikesNumber = $"{likesNumber} LIKES";
            _comment.Likes = likesNumber;
        }
    }
}
