using Instagram.Commands;
using Instagram.Databases;
using Instagram.Models;
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
        private int _userId;
        private CommentResponse _commentResponse;
        #region Commands
        public ICommand LikeButton { get; set; }
        public ICommand RemoveButton { get; set; }
        #endregion
        public ReplyCommentViewModel(CommentResponse commentResponse, int userId)
        {
            #region PrivatePropertiesAssignment
            _userId = userId;
            _commentResponse = commentResponse;
            _path = ConfigurationManager.AppSettings.Get("ResourcesPath");
            #endregion
            IsReplyYours = commentResponse.AuthorId == userId ? true : false;
            #region CommandInstances
            LikeButton = new LikeCommand(LikedThingsEnum.CommentResponse, commentResponse.AuthorId, commentResponse.CommentId, UpdateLikesNumber, ChangeIsUserLiked);
            RemoveButton = new RemoveCommentCommand();
            #endregion
            InitResources();
            LoadDataFromDatabaseAsync();
        }
        private void InitResources()
        {
            LikeIconPath = $"{_path}likeIcon.png";
            TrashIconPath = $"{_path}trashIcon.png";
        }
        private async Task LoadDataFromDatabaseAsync()
        {
            var LoadFromDatabaseAsync = async Task () =>
            {

                //using (var db = new InstagramDbContext("MainDb"))
                //{
                //    User profile = db.Users.First(u => u.Id == _commentResponse.AuthorId);
                //    CommentProfileName = profile.Nickname;
                //    ProfileImage profilePhoto = db.ProfileImages.First(p => p.UserId == profile.Id);
                //    CommentProfilePhotoSource = ConvertImage.FromByteArray(profilePhoto.ImageBytes);
                //    CommentText = _commentResponse.Content;
                //    PublicationDate = _commentResponse.PublicationDate;
                //    UpdateLikesNumber(_commentResponse.Likes);
                //    ChangeIsUserLiked(db.UsersLiked.Where(u => (
                //        u.UserThatLikedId == _userId &&
                //        u.LikedThingId == _commentResponse.Id &&
                //        (int)u.LikedThing == (int)LikedThingsEnum.CommentResponse
                //        )).ToList().Count());
                //}
            };
            await LoadFromDatabaseAsync.Invoke();
        }
        public void UpdateLikesNumber(int likesNumber)
        {
            LikesNumber = $"{likesNumber} LIKES";
            _commentResponse.Likes = likesNumber;
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
    }
}
