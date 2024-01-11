using Instagram.Commands;
using Instagram.Databases;
using Instagram.Models;
using Instagram.Services;
using Instagram.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private Comment _comment;
        public string LikeIconSource { get; set; } = @"C:\Programs\Instagram\Instagram\Resources\likeIcon.png";
        public string ReplyIconSource { get; set; } = @"C:\Programs\Instagram\Instagram\Resources\replyIcon.png";
        public string RemoveIconSource { get; set; } = @"C:\Programs\Instagram\Instagram\Resources\trashIcon.png";
        private string showMoreIconSource = @"C:\Programs\Instagram\Instagram\Resources\showMoreIcon.png";
        private string showLessIconSource = @"C:\Programs\Instagram\Instagram\Resources\showLessIcon.png";
        private bool showMoreCommentResponses = true;
        public BitmapImage CommentProfilePhotoSource { get; set; }
        public BitmapImage ReplyProfilePhotoSource { get; set; }
        public string CommentProfileName { get; set; }
        public string CommentText { get; set; }
        public DateTime PublicationDate { get; set; }
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
        private bool clicked = true;
        public ICommand LikeButton { get; set; }
        public ICommand ReplyButton { get; set; }
        public ICommand RemoveButton { get; set; }
        public ICommand CreateCommentReply { get; set; }
        public ICommand ShowMoreLessButton { get; set; }
        private int _userId;
        public CommentViewModel(Comment comment, int userId)
        {
            _comment = comment;
            _userId = userId;
            LikeButton = new LikeCommand(LikedThingsEnum.Comment, userId, comment.Id, UpdateLikesNumber, ChangeIsUserLiked);
            ReplyButton = new ReplyToCommentCommand(ChangeReplyClickedStatus);
            IsCommentYours = comment.AuthorId == userId ? true : false;
            RemoveButton = new RemoveCommentCommand();
            CreateCommentReply = new CreateCommentReplyCommand(CreateNewReply);
            ShowMoreLessButton = new ShowMoreLessCommentsCommand(LoadResponsesToComment);
            LoadDataFromDatabase();
        }
        public void LoadResponsesToComment()
        {
            CommentResponses = new ObservableCollection<ReplyCommentView>();
            if (showMoreCommentResponses && clicked)
            {
                ShowMoreLessButtonContent = showLessIconSource;
                using (var db = new InstagramDbContext())
                {
                    foreach (CommentResponse cm in db.CommentResponses.Where(c => c.CommentId == _comment.Id).ToList())
                    {
                        CommentResponses.Add(new ReplyCommentView(cm, _userId));
                    }
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
        private void LoadDataFromDatabase()
        {
            using (var db = new InstagramDbContext())
            {
                // do it easier
                User profile = db.Users.First(u => u.Id == _comment.AuthorId);
                CommentProfileName = profile.Nickname;
                ProfileImage profilePhoto = db.ProfileImages.First(p => p.UserId == profile.Id);
                CommentProfilePhotoSource = ConvertImage.FromByteArray(profilePhoto.ImageBytes);
                CommentText = _comment.Content;
                PublicationDate = _comment.PublicationDate;
                UpdateLikesNumber(_comment.Likes);
                ProfileImage userReplyCommentPhoto = db.ProfileImages.First(pi => pi.Id == _userId);
                ReplyProfilePhotoSource = ConvertImage.FromByteArray(userReplyCommentPhoto.ImageBytes);
                AreAnyComments = db.CommentResponses.Where(c => c.CommentId == _comment.Id).ToList().Count() > 0 ? true : false;
                ShowMoreLessButtonContent = showMoreIconSource;
                ChangeIsUserLiked(db.UsersLiked.Where(u => (
                    u.UserThatLikedId == _userId &&
                    u.LikedThingId == _comment.Id &&
                    (int)u.LikedThing == (int)LikedThingsEnum.Comment
                    )).ToList().Count());
            }
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
        public void CreateNewReply()
        {
            using (var db = new InstagramDbContext())
            {
                db.Comments.First(c => c.Id == _comment.Id).CommentResponses.Add(new CommentResponse()
                {
                    AuthorId = _userId,
                    Content = _ReplyCommentContent,
                    Likes = 0,
                    PublicationDate = DateTime.Now
                });
                db.SaveChanges();
                clicked = false;
                LoadResponsesToComment();
            }
            ChangeReplyClickedStatus();
        }
    }
}
