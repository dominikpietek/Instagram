using Instagram.Commands;
using Instagram.Databases;
using Instagram.Models;
using Instagram.Services;
using System;
using System.Collections.Generic;
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
        private CommentResponse _commentResponse;
        public string LikeIconPath { get; set; } = @"C:\Programs\Instagram\Instagram\Resources\likeIcon.png";
        public string TrashIconPath { get; set; } = @"C:\Programs\Instagram\Instagram\Resources\trashIcon.png";
        public BitmapImage CommentProfilePhotoSource { get; set; }
        public string CommentProfileName { get; set; }
        public string CommentText { get; set; }
        public DateTime PublicationDate { get; set; }
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
        private int _userId;
        public ICommand LikeButton { get; set; }
        public ICommand RemoveButton { get; set; }
        public ReplyCommentViewModel(CommentResponse commentResponse, int userId)
        {
            _userId = userId;
            _commentResponse = commentResponse;
            IsReplyYours = commentResponse.AuthorId == userId ? true : false;
            LikeButton = new LikeCommand(LikedThingsEnum.CommentResponse, commentResponse.AuthorId, commentResponse.CommentId, UpdateLikesNumber, ChangeIsUserLiked);
            RemoveButton = new RemoveCommentCommand();
            LoadDataFromDatabase();
        }

        private void LoadDataFromDatabase()
        {
            using (var db = new InstagramDbContext())
            {
                User profile = db.Users.First(u => u.Id == _commentResponse.AuthorId);
                CommentProfileName = profile.Nickname;
                ProfileImage profilePhoto = db.ProfileImages.First(p => p.UserId == profile.Id);
                CommentProfilePhotoSource = ConvertImage.FromByteArray(profilePhoto.ImageBytes);
                CommentText = _commentResponse.Content;
                PublicationDate = _commentResponse.PublicationDate;
                UpdateLikesNumber(_commentResponse.Likes);
                ChangeIsUserLiked(db.UsersLiked.Where(u => (
                    u.UserThatLikedId == _userId &&
                    u.LikedThingId == _commentResponse.Id &&
                    (int)u.LikedThing == (int)LikedThingsEnum.CommentResponse
                    )).ToList().Count());
            }
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
