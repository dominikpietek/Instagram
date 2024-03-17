using Instagram.Commands;
using Instagram.Components;
using Instagram.ComponentsViewModels;
using Instagram.Databases;
using Instagram.DTOs;
using Instagram.Interfaces;
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
        public ICommand LostFocus { get; set; }
        public ICommand ChangeSearching { get; set; }
        #endregion
        #region OnProperyChangeProperties
        private BitmapImage _ProfilePhotoSource;
        public BitmapImage ProfilePhotoSource 
        {
            get { return _ProfilePhotoSource; }
            set
            {
                _ProfilePhotoSource = value;
                OnPropertyChanged(nameof(ProfilePhotoSource));
            }
        }
        private bool _IsBarVisible;
        public bool IsBarVisible 
        { 
            get {  return _IsBarVisible; }
            set
            {
                _IsBarVisible = value;
                OnPropertyChanged(nameof(IsBarVisible));
            }
        }
        private string _SearchingText;
        public string SearchingText
        {
            get { return _SearchingText; }
            set
            {
                _SearchingText = value;
                OnPropertyChanged(nameof(SearchingText));
            }
        }
        private bool _IsFocused;
        public bool IsFocused
        {
            get { return _IsFocused; }
            set
            {
                _IsFocused = value;
                OnPropertyChanged(nameof(IsFocused));
            }
        }
        private bool _IsSearchClicked;
        public bool IsSearchClicked 
        { 
            get { return _IsSearchClicked; }
            set
            {
                _IsSearchClicked = value;
                OnPropertyChanged(nameof(IsSearchClicked));
            }
        }
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
        private ObservableCollection<SearchedUserView> _SearchedUsersSection = new ObservableCollection<SearchedUserView>();
        public ObservableCollection<SearchedUserView> SearchedUsersSection
        {
            get { return _SearchedUsersSection; }
            set
            {
                _SearchedUsersSection = value;
                OnPropertyChanged(nameof(SearchedUsersSection));
            }
        }
        private ObservableCollection<FriendRequestView> _FriendRequestSection;
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
        private ObservableCollection<StoryUserView> _StoriesSection;
        public ObservableCollection<StoryUserView> StoriesSection 
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
        private CheckProfileUserControl _checkProfileUserControl;
        private bool _isCheckProfileUCCreated = false;
        private readonly ResourceDictionary _resources;
        private readonly IAbstractFactory<CreateNewPostWindowView> _newPostFactory;
        private readonly IAbstractFactory<LoginOrRegisterWindowView> _loginFactory;
        private readonly IAbstractFactory<StoryUserView> _storyFactory;
        private readonly IUserRepository _userRepository;
        private readonly IStoryRepository _storyRepository;
        private readonly IGotSentFriendRequestModelRepository _userIdGotModelRepository;
        private readonly IGotSentFriendRequestModelRepository _userIdSentModelRepository;
        private readonly IFriendRepository _friendRepository;
        private readonly IAbstractFactory<HomeUserControl> _homeFactory;
        private readonly IAbstractFactory<ProfileUserControl> _profileFactory;
        private readonly IAbstractFactory<MessengerUserControl> _messengerFactory;
        private readonly IAbstractFactory<FriendRequestView> _friendRequestFactory;
        private readonly IAbstractFactory<MaybeFriendView> _maybeFriendFactory;
        private readonly IAbstractFactory<CheckProfileUserControl> _checkProfileFactory;
        private readonly IAbstractFactory<SearchedUserView> _searchedUserFactory;
        private SearchFilterRepository _SearchFilterRepository;
        private List<SearchUserDto> _searchUsersDtos;
        private readonly Func<bool> _IsMouseOverSearchingFriends;
        #endregion
        public FeedViewModel(
            Action CloseWindow,
            StackPanel feedViewMainContainer,
            ResourceDictionary resources,
            IAbstractFactory<CreateNewPostWindowView> newPostFactory,
            IAbstractFactory<LoginOrRegisterWindowView> loginFactory,
            IAbstractFactory<HomeUserControl> homeFactory,
            IAbstractFactory<StoryUserView> storyFactory,
            IAbstractFactory<ProfileUserControl> profileFactory,
            IAbstractFactory<MessengerUserControl> messengerFactory,
            IAbstractFactory<FriendRequestView> friendRequestFactory,
            IAbstractFactory<MaybeFriendView> maybeFriendFactory,
            IAbstractFactory<CheckProfileUserControl> checkProfileFactory,
            IAbstractFactory<SearchedUserView> searchedUserFactory,
            InstagramDbContext db,
            Func<bool> IsMouseOverSearchingFriends
            )
        {
            #region PrivatePropertiesAssignment
            _userRepository = new UserRepository(db);
            _storyRepository = new StoryRepository(db);
            _userIdGotModelRepository = new GotSentFriendRequestModelRepository<GotFriendRequestModel>(db);
            _userIdSentModelRepository = new GotSentFriendRequestModelRepository<SentFriendRequestModel>(db);
            _friendRepository = new FriendRepository(db);
            _path = ConfigurationManager.AppSettings.Get("ResourcesPath")!;
            _feedViewMainContainer = feedViewMainContainer;
            _resources = resources;
            _newPostFactory = newPostFactory;
            _loginFactory = loginFactory;
            _homeFactory = homeFactory;
            _storyFactory = storyFactory;
            _profileFactory = profileFactory;
            _messengerFactory = messengerFactory;
            _friendRequestFactory = friendRequestFactory;
            _maybeFriendFactory = maybeFriendFactory;
            _checkProfileFactory = checkProfileFactory;
            _searchedUserFactory = searchedUserFactory;
            _IsMouseOverSearchingFriends = IsMouseOverSearchingFriends;
            #endregion
            #region CommandsInstances
            CreateNewPost = new CreateNewPostOpenWindowCommand(_newPostFactory, UpdatePosts);
            LogoutButton = new LogoutCommand(CloseWindow, _loginFactory);
            HomeButton = new ChangeMainContainerContentCommand(ShowPosts);
            MessengerButton = new ChangeMainContainerContentCommand(ShowMessenger);
            ProfileButton = new ChangeMainContainerContentCommand(ShowProfile);
            DarkModeButton = new DarkModeCommand(ChangeThemes);
            SendEmailsButton = new SendEmailsCommand();
            SearchButton = new SearchCommand(ChangeIsSearchClickedValue);
            LostFocus = new ChangeMainContainerContentCommand(ChangeLostFocus);
            ChangeSearching = new ChangeSearchingCommand(GenerateSearchingUsers);
            #endregion
            InitResources();
            InitAsync();
        }
        #region Init
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

        private async Task InitAsync()
        {
            _user = await GetUser.FromDbAndFileAsync(_userRepository);
            _SearchFilterRepository = new SearchFilterRepository(await _userRepository.GetUsersIdAndNickaname());
            ProfilePhotoSource = ConvertImage.FromByteArray(_user.ProfilePhoto.ImageBytes);
            await LoadThemeColourFromJsonFileAsync();
            await LoadFriendRequestAsync();
            await ShowStoriesAsync();
            ShowPosts();
        }

        private async Task LoadThemeColourFromJsonFileAsync()
        {
            IsDarkModeOn = await ChangeTheme.GetModeAsync();
            await ChangeThemes(IsDarkModeOn);
        }

        public async Task ChangeThemes(bool isDarkMode)
        {
            await ChangeTheme.ChangeAsync(_resources);
            LogoPath = ChangeTheme.ChangeLogo(_path, isDarkMode);
            if (_isHomeUCCreated) await _homeUserControl.ChangeHomeTheme();
            if (_isProfileUCCreated) await _profileUserControl.ChangeProfileTheme();
            if (_isMessengerUCCreated) await _messengerUserControl.ChangeMessengerTheme();
            if (_isCheckProfileUCCreated) await _checkProfileUserControl.ChangeProfileTheme();
        }
        #endregion
        #region SearchingUsers
        public void GenerateSearchingUsers()
        {
            SearchedUsersSection = new ObservableCollection<SearchedUserView>();
            SearchedUsersSection = _SearchFilterRepository.GetMatchingProfiles(SearchingText, _searchedUserFactory, ShowCheckProfile);
            ChangeBarVisibility();
        }

        private void ChangeBarVisibility()
        {
            if (SearchedUsersSection.Count() <= 2) IsBarVisible = false;
            else IsBarVisible = true;
        }

        public void ChangeLostFocus()
        {
            if (!_IsMouseOverSearchingFriends.Invoke()) DeactivateSearch();
        }

        private void DeactivateSearch()
        {
            IsFocused = false;
            IsBarVisible = false;
            IsSearchClicked = false;
        }

        public void ChangeIsSearchClickedValue()
        {
            IsSearchClicked = true;
        }
        #endregion
        #region Stories
        private async Task ShowStoriesAsync()
        {
            StoriesSection = new ObservableCollection<StoryUserView>();
            var groupedStories = 
                (await _storyRepository.GetAllStoriesAsync())
                .Where(s => s.UserId != _user.Id)
                .GroupBy(s => s.UserId, s => s.Id, 
                (key, ids) => new { UserId = key, Ids = ids.ToList() });
            ShowStoryInBox(_user.Stories.Where(s => s.PublicationDate.AddHours(24) > DateTime.Now).Select(s => s.Id).ToList(), _user.Id);
            foreach (var story in groupedStories)
            {
                ShowStoryInBox(story.Ids, story.UserId);
            }
        }

        private void ShowStoryInBox(List<int> storyIds, int userId)
        {
            var storyBase = _storyFactory.Create();
            storyBase.SetDataContext(storyIds, userId);
            StoriesSection.Add(storyBase);
        }
        #endregion
        #region ShowInMainBox
        private void ShowSomethingInMainBox(UserControl userControl)
        {
            _feedViewMainContainer.Children.Clear();
            _feedViewMainContainer.Children.Add(userControl);
        }

        public void ShowCheckProfile(int profileUserId)
        {
            _checkProfileUserControl = _checkProfileFactory.Create();
            _checkProfileUserControl.SetDataContext(profileUserId);
            ShowSomethingInMainBox(_checkProfileUserControl);
            _isCheckProfileUCCreated = true;
            DeactivateSearch();
        }

        public void ShowPosts()
        {
            _homeUserControl = _homeFactory.Create();
            ShowSomethingInMainBox(_homeUserControl);
            _isHomeUCCreated = true;
        }

        public void ShowProfile()
        {
            _profileUserControl = _profileFactory.Create();
            ShowSomethingInMainBox(_profileUserControl);
            _isProfileUCCreated = true;
        }

        public void ShowMessenger()
        {
            _messengerUserControl = _messengerFactory.Create();
            ShowSomethingInMainBox(_messengerUserControl);
            _isMessengerUCCreated = true;
        }

        public async Task UpdatePosts()
        {
            HomeViewModel view = (HomeViewModel)_homeUserControl.DataContext!;
            await view.RefreshPosts();
        }
        #endregion
        #region Friends
        private async Task LoadMaybeFriendsAsync()
        {
            List<User> probablyFriendsAfterSelection = (await _userRepository.GetAllNotFriendsUsersAsync(_user.Id, _friendRepository, _userIdSentModelRepository)).TakeLast(7).ToList();
            MaybeFriendsSection = new ObservableCollection<MaybeFriendView>() {};
            foreach (User user in probablyFriendsAfterSelection)
            {
                var maybeView = _maybeFriendFactory.Create();
                maybeView.SetDataContext(user.Id, ShowCheckProfile);
                MaybeFriendsSection.Add(maybeView);
            }
            NoMaybeFriends();
        }

        private async Task LoadFriendRequestAsync()
        {
            FriendRequestSection = new ObservableCollection<FriendRequestView>();
            List<int> gotRequestPeopleIds = await _userIdGotModelRepository.GetAllAsync(_user.Id);
            foreach (int userThatSentRequestId in gotRequestPeopleIds)
            {
                var friendRequestView = _friendRequestFactory.Create();
                friendRequestView.SetDataContext(userThatSentRequestId, LoadFriendRequestAsync, ShowCheckProfile);
                FriendRequestSection.Add(friendRequestView);
                if (FriendRequestSection.Count == 7)
                {
                    break;
                }
            }
            await LoadMaybeFriendsAsync();
            NoFriendRequests();
        }

        private void NoFriendRequests()
        {
            if (FriendRequestSection.Count == 0) FriendRequestMessage = "No friend requests :(";
        }

        private void NoMaybeFriends()
        {
            if (MaybeFriendsSection.Count == 0) MaybeFriendsMessage = "There is no new users :(";
        }
        #endregion
    }
}
