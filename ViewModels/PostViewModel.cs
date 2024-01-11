using Instagram.Commands;
using Instagram.Databases;
using Instagram.Models;
using Instagram.Services;
using Instagram.Views;
using System;
using System.Collections;
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
    public class PostViewModel : ViewModelBase
    {
        private Post _post;
        public BitmapImage ProfilePhotoSource { get; set; }
        public BitmapImage PostPhotoSource { get; set; }
        public string LikeIconPath { get; set; } = @"C:\Programs\Instagram\Instagram\Resources\likeIcon.png";
        public string CommentIconPath { get; set; } = @"C:\Programs\Instagram\Instagram\Resources\commentIcon.png";
        public string MessageIconPath { get; set; } = @"C:\Programs\Instagram\Instagram\Resources\messageIcon.png";
        public string TrashIconPath { get; set; } = @"C:\Programs\Instagram\Instagram\Resources\trashIcon.png";
        public string ShowMoreIconPath { get; set; } = @"C:\Programs\Instagram\Instagram\Resources\showMoreIcon.png";
        public string ShowLessIconPath { get; set; } = @"C:\Programs\Instagram\Instagram\Resources\showLessIcon.png";
        private int _actualUserId;
        private bool _IsPostYour;
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
        //add round profile photo not square
        public string ProfileName { get; set; }
        public string Location { get; set; }
        private ObservableCollection<CommentView> _CommentsSection;
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
        public ICommand MoreComments { get; set; }
        public ICommand LessComments { get; set; }
        public ICommand LikeButton { get; set; }
        public ICommand CommentButton { get; set; }
        public ICommand MessageButton { get; set; }
        public ICommand CreateComment { get; set; }
        public ICommand DeletePost { get; set; }
        public PostViewModel(Post post, int actualUserId, Action ShowPosts)
        {
            _post = post;
            _actualUserId = actualUserId;
            MoreComments = new ShowMoreLessCommentsCommand(ShowMoreLessCommentsChange);
            LessComments = new ShowMoreLessCommentsCommand(ShowMoreLessCommentsChange);
            LikeButton = new LikeCommand(LikedThingsEnum.Post, actualUserId, post.Id, UpdateLikesNumber, ChangeIsPostLiked);
            CommentButton = new CommentButtonCommand(UpdateIsCommentClickedToCreateValue);
            CreateComment = new CommentCreateCommand(CreateNewComment);
            MessageButton = new OpenCommunicatorWindowCommand();
            DeletePost = new DeletePostCommand(post, ShowPosts);
            LoadDataFromDatabase();
        }
        private void LoadDataFromDatabase()
        {
            using (var db = new InstagramDbContext())
            {
                ProfilePhotoSource = ConvertImage.FromByteArray(db.ProfileImages.First(p => p.UserId == _post.UserId).ImageBytes);
                ProfileName = db.Users.First(p => p.Id == _post.UserId).Nickname;
                PostPhotoSource = ConvertImage.FromByteArray(db.PostImages.First(p => p.PostId == _post.Id).ImageBytes);
                Location = _post.Location == null ? "" : _post.Location;
                _IsPostYour = _post.UserId == _actualUserId ? true : false;
                UpdateCommentsNumber(db.Comments.Where(c => c.PostId == _post.Id).ToList().Count);
                ChangeIsPostLiked(db.UsersLiked.Where(u => u.UserThatLikedId == _actualUserId && u.LikedThingId == _post.Id && (int)u.LikedThing == (int)LikedThingsEnum.Post).ToList().Count());
                UpdateLikesNumber(_post.Likes);
            }
        }
        private void UpdateCommentsNumber(int commentsNumber)
        {
            CommentsNumber = $"{commentsNumber} COMMENTS";
        }
        public void UpdateLikesNumber(int newLikesNumber)
        {
            LikesNumber = $"{newLikesNumber} LIKES";
            _post.Likes = newLikesNumber;
        }
        public void UpdateComments(InstagramDbContext db)
        {
            CommentsSection = new ObservableCollection<CommentView>();
            foreach (var comment in db.Posts.Where(p => p.Id == _post.Id).SelectMany(p => p.Comments))
            {
                CommentsSection.Add(new CommentView(comment, _actualUserId));
            }
            UpdateCommentsNumber(_CommentsSection.Count());
        }
        public void ShowMoreLessCommentsChange()
        {
            ShowMoreComments ^= true;
            if (ShowMoreComments)
            {
                using (var db = new InstagramDbContext())
                {
                    UpdateComments(db);
                }
            }
        }
        public void UpdateIsCommentClickedToCreateValue()
        {
            IsCommentClickedToCreate ^= true;
        }
        public void CreateNewComment()
        {
            using (var db = new InstagramDbContext())
            {
                db.Posts.Where(p => p.Id == _post.Id).ToList().ForEach(p => {
                    p.Comments.Add(new Comment()
                    {
                        AuthorId = _actualUserId,
                        Content = _CommentContent,
                        Likes = 0,
                        PublicationDate = DateTime.Now
                    });
                });
                db.SaveChanges();
                UpdateComments(db);
            }
            IsCommentClickedToCreate = false;
        }
        public void ChangeIsPostLiked(int isUserLikedCount)
        {
            if (isUserLikedCount == 1)
            {
                IsPostLiked = true;
            }
            else
            {
                IsPostLiked = false;
            }
        }
    }
}
