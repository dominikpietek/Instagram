using Instagram.Commands;
using Instagram.Models;
using Instagram.Services;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Instagram.ViewModels
{
    public class CreatePostViewModel : ViewModelBase
    {
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
        private bool _OnlyForFriends = false;
        public bool OnlyForFriends 
        { 
            get { return _OnlyForFriends; } 
            set 
            { 
                _OnlyForFriends= value;
                OnPropertyChanged(nameof(OnlyForFriends));
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
        public ICommand SubmitCreatingNewPost { get; set; }
        public ICommand OpenImageButton { get; set; }
        private Action _CloseWindow;
        private User _user;
        public CreatePostViewModel(Action CloseWindow, User user)
        {
            _CloseWindow = CloseWindow;
            _user = user;
            SubmitCreatingNewPost = new SubmitCreatingNewPostCommand(CloseWindow, CreatePost);
            OpenImageButton = new OpenImageButtonCommand(OnLoadingImage);
        }
        public void CloseWindow()
        {
            _CloseWindow.Invoke();
        }
        public void OnLoadingImage(string imagePath)
        {
            _ImageSource = imagePath;
        }
        public void ModifyTagsStringToList()
        {

        }
        public void CreatePost()
        {
            // add validation
            Post post = new Post()
            {
                Description = _Description,
                Location = _Location,
                Image = new PostImage() { ImageBytes = ConvertImage.ToByteArray(_ImageSource) },
                PublicationDate = DateTime.Now,
                OnlyForFriends = _OnlyForFriends,
                Tags = new List<Tag>()
            };
            _user.Posts.Add(post);
            NewPost.Create(_user.Id, post);
        }
    }
}
