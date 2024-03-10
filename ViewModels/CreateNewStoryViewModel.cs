using Instagram.Commands;
using Instagram.Databases;
using Instagram.Interfaces;
using Instagram.Migrations;
using Instagram.Models;
using Instagram.Repositories;
using Instagram.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Instagram.ViewModels
{
    public class CreateNewStoryViewModel : ViewModelBase
    {
        #region OnPropertyChangedProperties
        private string _ImageSource;
        public string ImageSource
        {
            get { return _ImageSource; }
            set
            {
                _ImageSource = value;
                OnPropertyChanged(nameof(ImageSource));
            }
        }
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
        #endregion
        #region CommandProperties
        public ICommand OpenImageButton { get; set; }
        public ICommand PublishButton { get; set; }
        #endregion
        #region PrivateProperties
        private readonly IStoryRepository _storyRepository;
        private readonly string _path;
        private int _userId;
        #endregion
        public CreateNewStoryViewModel(InstagramDbContext db, Action CloseWindow)
        {
            #region Commands
            OpenImageButton = new OpenImageButtonCommand(OnLoadingImage);
            PublishButton = new SubmitCreatingNewPostCommand(CloseWindow, PublishStories);
            #endregion
            #region PrivatePropertiesAssignement
            _storyRepository = new StoryRepository(db);
            _path = ConfigurationManager.AppSettings["ResourcesPath"]!;
            #endregion
            InitAsync();
        }

        private async Task InitAsync() 
        {
            ImageSource = $"{_path}noImageIcon.png";
            _userId = await GetUser.IdFromFile();
        }

        public void OnLoadingImage(string imagePath)
        {
            ImageSource = imagePath;
        }

        public async Task<bool> PublishStories()
        {
            //update stories
            if (ImageSource.Equals($"{_path}noImageIcon.png"))
            {
                ImageErrorMessage = "You have to import image";
                return false;
            }
            Story story = new Story()
            {
                UserId = _userId,
                Image = new StoryImage() { ImageBytes = ConvertImage.ToByteArray(_ImageSource) },
                PublicationDate = DateTime.Now,
            };
            await _storyRepository.AddStoryAsync(_userId, story);
            return true;
        }
    }
}
