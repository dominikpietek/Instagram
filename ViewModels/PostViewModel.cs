using Instagram.Commands;
using Instagram.Databases;
using Instagram.Enums;
using Instagram.JSONModels;
using Instagram.Models;
using Instagram.Services;
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
        //add round profile photo not square
        #region NormalProperties
        public string ProfileName { get; set; }
        public string Location { get; set; }
        #endregion
        #region PrivateProperties
        private Post _post;
        private int _actualUserId;
        private bool _IsPostYour;
        private bool _isDarkMode;
        private InstagramDbContext _db;
        #endregion
        public PostViewModel(Post post, Func<Task> ShowPosts)
        {
            #region PrivatePropertiesAssignment
            _post = post;
            _path = ConfigurationManager.AppSettings.Get("ResourcesPath");
            #endregion
            LoadDataFromDatabaseAsync();
            InitResources();
            #region CommandsInstances
            MoreComments = new ShowMoreLessCommentsCommand(ShowMoreLessCommentsChangeAsync, ChangeTheme);
            LessComments = new ShowMoreLessCommentsCommand(ShowMoreLessCommentsChangeAsync, ChangeTheme);
            //LikeButton = new LikeCommand(LikedThingsEnum.Post, actualUserId, post.Id, UpdateLikesNumber, ChangeIsPostLiked);
            CommentButton = new CommentButtonCommand(UpdateIsCommentClickedToCreateValue);
            CreateComment = new CommentCreateCommand(CreateNewCommentAsync);
            MessageButton = new OpenCommunicatorWindowCommand();
            DeletePost = new DeletePostCommand(post, ShowPosts);
            #endregion
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
        private string ConvertTagsToString(InstagramDbContext db)
        {
            var tagsStringList = db.Tags.Where(t => t.PostId == _post.Id).Select(p => p.Text).ToList();
            string converted = "";
            foreach (var tag in tagsStringList)
            {
                converted = $"{converted} #{tag}";
            }
            return converted;
        }
        private async Task LoadDataFromDatabaseAsync()
        {
            var LoadFromDatabaseAsync = async Task () =>
            {

                //using (var db = new InstagramDbContext("MainDb"))
                //{
                //    ProfilePhotoSource = ConvertImage.FromByteArray(db.ProfileImages.First(p => p.UserId == _post.UserId).ImageBytes);
                //    ProfileName = db.Users.First(p => p.Id == _post.UserId).Nickname;
                //    PostPhotoSource = ConvertImage.FromByteArray(db.PostImages.First(p => p.PostId == _post.Id).ImageBytes);
                //    Tags = ConvertTagsToString(db);
                //    Description = _post.Description;
                //    Location = _post.Location == null ? "" : _post.Location;
                //    _IsPostYour = _post.UserId == _actualUserId ? true : false;
                //    UpdateCommentsNumber(db.Comments.Where(c => c.PostId == _post.Id).ToList().Count);
                //    ChangeIsPostLiked(db.UsersLiked.Where(u => u.UserThatLikedId == _actualUserId && u.LikedThingId == _post.Id && (int)u.LikedThing == (int)LikedThingsEnum.Post).ToList().Count());
                //    UpdateLikesNumber(_post.Likes);
                //}
            };
            await LoadFromDatabaseAsync.Invoke();
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
        private async Task GetDarkModeFromJsonFileAsync()
        {
            var json = new JSON<UserDataModel>("UserData");
            _isDarkMode = await json.GetDarkModeAsync();
        }
        public void ChangeTheme()
        {
            GetDarkModeFromJsonFileAsync();
            if (CommentsSection != null)
            {
                foreach (var comment in CommentsSection)
                {
                    comment.ChangeCommentTheme(_isDarkMode);
                }
            }
        }
        public void UpdateComments(InstagramDbContext db)
        {
            CommentsSection = new ObservableCollection<CommentView>();
            foreach (var comment in db.Posts.Where(p => p.Id == _post.Id).SelectMany(p => p.Comments))
            {
                CommentsSection.Add(new CommentView(comment, _actualUserId, _db));
            }
            UpdateCommentsNumber(_CommentsSection.Count());
        }
        public async Task ShowMoreLessCommentsChangeAsync()
        {
            ShowMoreComments ^= true;
            if (ShowMoreComments)
            {
                var UpdateDatabaseAsync = async Task () =>
                {
                    //using (var db = new InstagramDbContext("MainDb"))
                    //{
                    //    UpdateComments(db);
                    //}
                };
                await UpdateDatabaseAsync.Invoke();
            }
        }
        public void UpdateIsCommentClickedToCreateValue()
        {
            IsCommentClickedToCreate ^= true;
        }
        public async Task CreateNewCommentAsync()
        {
            var UpdateDatabaseAsync = async Task () =>
            {
                //using (var db = new InstagramDbContext("MainDb"))
                //{
                //    db.Posts.Where(p => p.Id == _post.Id).ToList().ForEach(p =>
                //    {
                //        p.Comments.Add(new Comment()
                //        {
                //            AuthorId = _actualUserId,
                //            Content = _CommentContent,
                //            Likes = 0,
                //            PublicationDate = DateTime.Now
                //        });
                //    });
                //    db.SaveChanges();
                //    UpdateComments(db);
                //}
            };
            await UpdateDatabaseAsync.Invoke();
            IsCommentClickedToCreate = false;
        }
        public void ChangeIsPostLiked(bool isUserLikedCount)
        {
            if (isUserLikedCount)
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
