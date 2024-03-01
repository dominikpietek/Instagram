using Instagram.Commands;
using Instagram.Databases;
using Instagram.Interfaces;
using Instagram.JSONModels;
using Instagram.Models;
using Instagram.Repositories;
using Instagram.Services;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Instagram.ViewModels
{
    public class CreatePostViewModel : ViewModelBase
    {
        #region OnChangePropertyProperties
        private string _ImageErrorMessage;
        public string ImageErrorMessage
        {
            get { return _ImageErrorMessage; }
            set
            {
                _ImageErrorMessage = value;
                OnPropertyChanged(nameof(ImageErrorMessage));
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
        private string _Tags;
        public string Tags 
        { 
            get { return _Tags; } 
            set 
            { 
                _Tags= value;
                OnPropertyChanged(nameof(Tags));
            }
        }
        private string _ImageSource;
        public string ImageSource { 
            get { return _ImageSource; } 
            set {
                _ImageSource = value;
                OnPropertyChanged(nameof(ImageSource));
            } 
        }
        private string _Location;
        public string Location 
        { 
            get { return _Location; }
            set
            {
                _Location = value;
                OnPropertyChanged(nameof(Location));
            }
        }
        #endregion
        #region Commands
        public ICommand SubmitCreatingNewPost { get; set; }
        public ICommand OpenImageButton { get; set; }
        #endregion
        #region PrivateProperties
        private readonly Action _CloseWindow;
        private User _user;
        private readonly string _path;
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;
        #endregion
        public CreatePostViewModel(Action CloseWindow, InstagramDbContext db)
        {
            #region PrivatePropertiesAssignement
            _path = ConfigurationManager.AppSettings.Get("ResourcesPath");
            _userRepository = new UserRepository(db);
            _postRepository = new PostRepository(db);
            _CloseWindow = CloseWindow;
            #endregion
            InitUser();
            InitResources();
            #region CommandsInstances
            SubmitCreatingNewPost = new SubmitCreatingNewPostCommand(CloseWindow, CreatePost);
            OpenImageButton = new OpenImageButtonCommand(OnLoadingImage);
            #endregion
        }
        private async Task InitUser()
        {
            _user = await GetUser.FromDbAndFileAsync(_userRepository);
        }
        public void CloseWindow()
        {
            _CloseWindow.Invoke();
        }
        private void InitResources()
        {
            ImageSource = $"{_path}noImageIcon.png";
        }
        public void OnLoadingImage(string imagePath)
        {
            ImageSource = imagePath;
        }
        private List<Tag> ModifyTagsStringToList()
        {
            List<string> listOfTags = Tags.Split(" ").Where(t => t != "").ToList();
            List<Tag> tagList = new List<Tag>();
            foreach (var tagText in listOfTags)
            {
                tagList.Add(new Tag()
                {
                    Text = tagText
                });
            }
            return tagList;
        }
        public bool CreatePost()
        {
            if (ImageSource.Equals($"{_path}noImageIcon.png"))
            {
                ImageErrorMessage = "You have to import image";
                return false;
            }
            Post post = new Post()
            {
                UserId = _user.Id,
                Description = _Description,
                Location = _Location,
                Image = new PostImage() { ImageBytes = ConvertImage.ToByteArray(_ImageSource) },
                PublicationDate = DateTime.Now,
                Tags = ModifyTagsStringToList()
            };
            _postRepository.AddPostAsync(post);
            return true;
        }
    }
}
