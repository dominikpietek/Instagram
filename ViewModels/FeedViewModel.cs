using Instagram.Commands;
using Instagram.Components;
using Instagram.Databases;
using Instagram.DTOs;
using Instagram.Interfaces;
using Instagram.JSONModels;
using Instagram.Models;
using Instagram.Repositories;
using Instagram.Services;
using Instagram.StartupHelpers;
using Instagram.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Instagram.ViewModels
{
    public class FeedViewModel : ViewModelBase
    {
        #region Resources
        private readonly string _path;
        public BitmapImage ProfilePhotoSource { get; set; }
        public string LogoutPath { get; set; }
        public string MessageIconPath { get; set; }
        public string HomeIconPath { get; set; }
        public string SearchIconPath { get; set; }
        #endregion
        #region Commands
        public ICommand HomeButton { get; set; }
        public ICommand MessengerButton { get; set; }
        public ICommand SearchButton { get; set; }
        public ICommand ProfileButton { get; set; }
        public ICommand LogoutButton { get; set; }
        public ICommand CreateNewPost { get; set; }
        public ICommand DarkModeButton { get; set; }
        public ICommand SendEmailsButton { get; set; }
        #endregion
        #region OnProperyChangeProperties
        private string _FriendRequestMessage;
        public string FriendRequestMessage 
        { 
            get { return _FriendRequestMessage; }
            set
            {
                _FriendRequestMessage = value;
                OnPropertyChanged(nameof(FriendRequestMessage));
            }
        }
        private string _MaybeFriendsMessage;
        public string MaybeFriendsMessage 
        { 
            get { return _MaybeFriendsMessage; }
            set
            {
                _MaybeFriendsMessage = value;
                OnPropertyChanged(nameof(MaybeFriendsMessage));
            }
        }
        private string _LogoPath;
        public string LogoPath
        {
            get { return _LogoPath; }
            set
            {
                _LogoPath = value;
                OnPropertyChanged(nameof(LogoPath));
            }
        }
        private bool _IsDarkModeOn;
        public bool IsDarkModeOn 
        { 
            get {  return _IsDarkModeOn; }
            set
            {
                _IsDarkModeOn = value;
                OnPropertyChanged(nameof(IsDarkModeOn));
            }
        }
        private ObservableCollection<FriendRequestView> _FriendRequestSection { get; set; }
        public ObservableCollection<FriendRequestView> FriendRequestSection 
        {
            get { return _FriendRequestSection; }
            set
            {
                _FriendRequestSection = value;
                OnPropertyChanged(nameof(FriendRequestSection));
            }
        }
        private ObservableCollection<MaybeFriendView> _MaybeFriendsSection;
        public ObservableCollection<MaybeFriendView> MaybeFriendsSection 
        {
            get { return _MaybeFriendsSection; }
            set
            {
                _MaybeFriendsSection = value;
                OnPropertyChanged(nameof(MaybeFriendsSection));
            }
        }
        private ObservableCollection<StoryView> _StoriesSection;
        public ObservableCollection<StoryView> StoriesSection 
        {
            get { return _StoriesSection; }
            set
            {
                _StoriesSection = value;
                OnPropertyChanged(nameof(StoriesSection));
            }
        }
        #endregion
        #region PrivateProperties
        private User _user;
        private StackPanel _feedViewMainContainer;
        private HomeUserControl _homeUserControl;
        private bool _isHomeUCCreated = false;
        private ProfileUserControl _profileUserControl;
        private bool _isProfileUCCreated = false;
        private MessengerUserControl _messengerUserControl;
        private bool _isMessengerUCCreated = false;
        private ResourceDictionary _resources;
        private IAbstractFactory<CreateNewPostWindowView> _newPostFactory;
        private IAbstractFactory<LoginOrRegisterWindowView> _loginFactory;
        private InstagramDbContext _db;
        private IUserRepository _userRepository;
        private IStoryRepository _storyRepository;
        private IUserIdGotSentModelRepository _userIdGotModelRepository;
        private IUserIdGotSentModelRepository _userIdSentModelRepository;
        private IFriendRepository _friendRepository;
        private IAbstractFactory<HomeUserControl> _homeFactory;
        #endregion
        public FeedViewModel(
            Action CloseWindow,
            StackPanel feedViewMainContainer,
            ResourceDictionary resources,
            IAbstractFactory<CreateNewPostWindowView> newPostFactory,
            IAbstractFactory<LoginOrRegisterWindowView> loginFactory,
            IAbstractFactory<HomeUserControl> homeFactory,
            InstagramDbContext db)
        {
            #region PrivatePropertiesAssignment
            _db = db;
            _userRepository = new UserRepository(_db);
            _storyRepository = new StoryRepository(_db);
            _userIdGotModelRepository = new UserIdGotSentModelsRepository<UserIdGotModel>(_db);
            _userIdSentModelRepository = new UserIdGotSentModelsRepository<UserIdSentModel>(_db);
            _friendRepository = new FriendRepository(_db);
            _path = ConfigurationManager.AppSettings.Get("ResourcesPath");
            _feedViewMainContainer = feedViewMainContainer;
            _resources = resources;
            _newPostFactory = newPostFactory;
            _loginFactory = loginFactory;
            _homeFactory = homeFactory;
            #endregion
            #region CommandsInstances
            CreateNewPost = new CreateNewPostOpenWindowCommand(_newPostFactory);
            LogoutButton = new LogoutCommand(CloseWindow, _loginFactory);
            HomeButton = new ChangeMainContainerContentCommand(ShowPosts);
            MessengerButton = new ChangeMainContainerContentCommand(ShowMessenger);
            ProfileButton = new ChangeMainContainerContentCommand(ShowProfile);
            DarkModeButton = new DarkModeCommand(ChangeThemes);
            SendEmailsButton = new SendEmailsCommand();
            #endregion
            InitResources();
            InitWithDbAsync();
        }

        private async Task InitWithDbAsync()
        {
            _user = await GetUser.FromDbAndFileAsync(_userRepository);
            await LoadThemeColourFromJsonFileAsync();
            await LoadEverythingFromDatabaseAsync();
            ShowPosts();
        }

        private void InitResources()
        {
            LogoPath = ChangeTheme.ChangeLogo(_path, IsDarkModeOn);
            LogoutPath = $"{_path}logoutIcon.png";
            MessageIconPath = $"{_path}messageIcon.png";
            HomeIconPath = $"{_path}homeIcon.png";
            SearchIconPath = $"{_path}searchIcon.png";
            FriendRequestMessage = "Friend requests:";
            MaybeFriendsMessage = "New users, maybe you know them?:";
        }

        public void ChangeThemes(bool isDarkMode)
        {
            ChangeTheme.ChangeAsync(_resources);
            LogoPath = ChangeTheme.ChangeLogo(_path, isDarkMode);
            if (_isHomeUCCreated) _homeUserControl.ChangeHomeTheme();
            if (_isProfileUCCreated) _profileUserControl.ChangeProfileTheme(isDarkMode);
            if (_isMessengerUCCreated) _messengerUserControl.ChangeMessengerTheme(isDarkMode);
        }

        private async Task LoadThemeColourFromJsonFileAsync()
        {
            IsDarkModeOn = await ChangeTheme.GetModeAsync();
            ChangeThemes(IsDarkModeOn);
        }

        private async Task LoadEverythingFromDatabaseAsync()
        {
            ProfilePhotoSource = ConvertImage.FromByteArray(_user.ProfilePhoto.ImageBytes);
            LoadFriendRequestAsync(_user.Id);
            //await ShowStoriesAsync();
        }

        private void ShowSomethingInMainBox(UserControl userControl)
        {
            _feedViewMainContainer.Children.Clear();
            _feedViewMainContainer.Children.Add(userControl);
        }

        private async Task ShowStoriesAsync()
        {
            StoriesSection = new ObservableCollection<StoryView>();
            var stories = await _storyRepository.GetAllStoriesAsync();
            foreach (var story in stories)
            {
                StoriesSection.Add(new StoryView(story));
            }
        }

        private void ShowPosts()
        {
            _homeUserControl = _homeFactory.Create();
            ShowSomethingInMainBox(_homeUserControl);
            _isHomeUCCreated = true;
        }

        private void ShowProfile()
        {
            _profileUserControl = new ProfileUserControl();
            ShowSomethingInMainBox(_profileUserControl);
            _isProfileUCCreated = true;
        }

        private void ShowMessenger()
        {
            _messengerUserControl = new MessengerUserControl(_user.Id);
            ShowSomethingInMainBox(_messengerUserControl);
            _isMessengerUCCreated = true;
        }
        private async void LoadMaybeFriendsAsync(int userId)
        {
            List<User> probablyFriendsAfterSelection = await _userRepository.GetAllNotFriendsUsersAsync(userId, _friendRepository, _userIdSentModelRepository);
            probablyFriendsAfterSelection = probablyFriendsAfterSelection.TakeLast(7).ToList();
            MaybeFriendsSection = new ObservableCollection<MaybeFriendView>() {};
            foreach (User user in probablyFriendsAfterSelection)
            {
                MaybeFriendsSection.Add(new MaybeFriendView(new FriendDto() 
                { 
                    Id = user.Id,
                    Nickname = user.Nickname,
                    ProfilePhoto = user.ProfilePhoto
                }, userId, LoadMaybeFriendsAsync) { });
            }
            if (MaybeFriendsSection.Count == 0)
            {
                MaybeFriendsMessage = "There is no new users :(";
            }
        }
        private async void LoadFriendRequestAsync(int userId)
        {
            FriendRequestSection = new ObservableCollection<FriendRequestView>();
            List<int> gotRequestPeopleIds = await _userIdGotModelRepository.GetAllAsync(userId);
            foreach (int userThatSentRequestId in gotRequestPeopleIds)
            {
                User user = await _userRepository.GetUserWithPhotoAndRequestsAsync(userThatSentRequestId);
                FriendRequestSection.Add(new FriendRequestView(new FriendDto()
                {
                    Id = userThatSentRequestId,
                    ProfilePhoto = user.ProfilePhoto,
                    Nickname = user.Nickname,
                }, userId, LoadFriendRequestAsync));
                if (FriendRequestSection.Count == 7)
                {
                    break;
                }
            }
            LoadMaybeFriendsAsync(_user.Id);
            if (FriendRequestSection.Count == 0)
            {
                FriendRequestMessage = "No friend requests :(";
            }
        }
    }
}
