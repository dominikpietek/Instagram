using Instagram.Commands;
using Instagram.Databases;
using Instagram.Interfaces;
using Instagram.Models;
using Instagram.Repositories;
using Instagram.Services;
using Instagram.StartupHelpers;
using Instagram.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Instagram.ViewModels
{
    public class StoryUserViewModel : ViewModelBase
    {
        #region PrivateProperties
        private readonly int _userId;
        private readonly string _path;
        private readonly List<int> _storyIds;
        private readonly IUserRepository _userRepository;
        #endregion
        #region NormalProperties
        public string PlusIconSource { get; set; }
        public BitmapImage ProfilePhotoSource { get; set; }
        #endregion
        #region Commands
        public ICommand ShowStory { get; set; }
        public ICommand CreateStory { get; set; }
        #endregion
        #region OnPropertyChangedProperty
        private bool _IsPlusUsed;
        public bool IsPlusUsed 
        { 
            get { return _IsPlusUsed; }
            set
            {
                _IsPlusUsed = value;
                OnPropertyChanged(nameof(IsPlusUsed));
            }
        }
        #endregion
        public StoryUserViewModel(InstagramDbContext db, List<int> storyIds, IAbstractFactory<StoryView> storyFactory, IAbstractFactory<CreateNewStoryView> createStoryFactory, int userId)
        {
            #region PrivatePropertiesAssignment
            _userId = userId;
            _path = ConfigurationManager.AppSettings["ResourcesPath"];
            _storyIds = storyIds;
            _userRepository = new UserRepository(db);
            #endregion
            #region Commands
            ShowStory = new ShowStoryCommand(storyIds, storyFactory, userId, ChangePlus);
            CreateStory = new CreateStoryCommand(createStoryFactory, CreateStoriesFictional);
            #endregion
            SourcesInitAsync();
        }

        private async Task SourcesInitAsync()
        {
            PlusIconSource = $"{_path}/plusIcon.png";
            User user = await _userRepository.GetUserWithPhotoAndRequestsAsync(_userId);
            ProfilePhotoSource = ConvertImage.FromByteArray(user.ProfilePhoto.ImageBytes);
            if (_storyIds.Count == 0)
            {
                IsPlusUsed = true;
            }
            else
            {
                IsPlusUsed = false;
            }
        }

        public async Task CreateStoriesFictional(bool add)
        {
            User user = await _userRepository.GetUserWithPhotoAndRequestsAsync(_userId);
            _storyIds.Add(user.Stories.Last().Id);
            ChangePlus();
        }

        public void ChangePlus()
        {
            IsPlusUsed ^= true;
        }
    }
}
