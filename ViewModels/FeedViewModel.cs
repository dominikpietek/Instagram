using Instagram.Commands;
using Instagram.Components;
using Instagram.Databases;
using Instagram.DTOs;
using Instagram.JSONModels;
using Instagram.Models;
using Instagram.Services;
using Instagram.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
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
        private User _user;
        private InstagramDbContext _db { get; set; }
        private StackPanel _feedViewMainContainer;
        private Action<bool> _ChangeLoginTheme;
        private Action<bool> _ChangeFeedTheme;
        private HomeUserControl _homeUserControl;
        private bool _isHomeUCCreated = false;
        private ProfileUserControl _profileUserControl;
        private bool _isProfileUCCreated = false;
        private MessengerUserControl _messengerUserControl;
        private bool _isMessengerUCCreated = false;
        public FeedViewModel(User user, Action CloseWindow, StackPanel feedViewMainContainer, Action<bool> ChangeLoginTheme, Action<bool> ChangeFeedTheme)
        {
            #region PrivatePropertiesAssignment
            _user = user;
            _path = ConfigurationManager.AppSettings.Get("ResourcesPath");
            _feedViewMainContainer = feedViewMainContainer;
            _ChangeLoginTheme = ChangeLoginTheme;
            _ChangeFeedTheme = ChangeFeedTheme;
            #endregion
            #region CommandsInstances
            CreateNewPost = new CreateNewPostOpenWindowCommand(user, ShowPosts);
            LogoutButton = new LogoutCommand(CloseWindow);
            HomeButton = new ChangeMainContainerContentCommand(ShowPosts);
            MessengerButton = new ChangeMainContainerContentCommand(ShowMessenger);
            ProfileButton = new ChangeMainContainerContentCommand(ShowProfile);
            DarkModeButton = new DarkModeCommand(ChangeThemes);
            SendEmailsButton = new SendEmailsCommand();
            #endregion
            InitResources();
            LoadThemeColourFromJsonFileAsync();
            ShowPosts();
            LoadEverythingFromDatabaseAsync();
        }
        private void InitResources()
        {
            ChooseLogo(IsDarkModeOn);
            LogoutPath = $"{_path}logoutIcon.png";
            MessageIconPath = $"{_path}messageIcon.png";
            HomeIconPath = $"{_path}homeIcon.png";
            SearchIconPath = $"{_path}searchIcon.png";
            FriendRequestMessage = "Friend requests:";
            MaybeFriendsMessage = "New users, maybe you know them?:";
        }
        private void ChooseLogo(bool isDarkMode)
        {
            if (isDarkMode)
            {
                LogoPath = $"{_path}/darkLogo.png";
            }
            else
            {
                LogoPath = $"{_path}/logo.png";
            }
        }
        private void ChangeThemes(bool isDarkMode)
        {
            _ChangeLoginTheme.Invoke(isDarkMode);
            _ChangeFeedTheme.Invoke(isDarkMode);
            if (_isHomeUCCreated) _homeUserControl.ChangeHomeTheme(isDarkMode);
            if (_isProfileUCCreated) _profileUserControl.ChangeProfileTheme(isDarkMode);
            if (_isMessengerUCCreated) _messengerUserControl.ChangeMessengerTheme(isDarkMode);
            ChooseLogo(isDarkMode);
        }
        private async Task LoadThemeColourFromJsonFileAsync()
        {
            JSON<UserDataModel> userJSON = new JSON<UserDataModel>("UserData");
            bool isDarkMode = await userJSON.GetDarkModeAsync();
            IsDarkModeOn = isDarkMode;
            ChangeThemes(isDarkMode);
        }
        private async Task LoadEverythingFromDatabaseAsync()
        {
            var LoadFromDatabaseAsync = async Task () =>
            {
                using (_db = new InstagramDbContext("MainDb"))
                {
                    LoadProfilePhoto();
                    ShowStories(_db);
                    LoadFriendRequest(_db, _user.Id);
                }
            };
            await LoadFromDatabaseAsync.Invoke();
        }
        private void LoadProfilePhoto()
        {
            var profilePhoto = _db.ProfileImages.First(p => p.UserId == _user.Id);
            ProfilePhotoSource = ConvertImage.FromByteArray(profilePhoto.ImageBytes);
        }
        private void ShowSomethingInMainBox(UserControl userControl)
        {
            _feedViewMainContainer.Children.Clear();
            _feedViewMainContainer.Children.Add(userControl);
        }
        private void ShowStories(InstagramDbContext db)
        {
            //List<Friend> variable = db.Users.Where(u => u.Id == _user.Id).Include(u => u.Friends).SelectMany(u => u.Friends).ToList();
            //StoriesSection = ;
        }
        private void ShowPosts()
        {
            _homeUserControl = new HomeUserControl(_user.Id);
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
        private void LoadMaybeFriends(InstagramDbContext db, int userId)
        {
            List<int> userFriendsIds = db.Friends.Where(f => f.UserId == userId).Select(f => f.FriendId).ToList();
            List<int> sentRequestPeople = db.UserIdSentModels.Where(uism => uism.UserId == userId).Select(uism => uism.StoredUserId).ToList();
            List<User> probablyFriendsAfterSelection = db.Users.Where(u =>
                u.Id != userId &&
                !userFriendsIds.Contains(u.Id) &&
                !sentRequestPeople.Contains(u.Id)
                ).ToList();
            probablyFriendsAfterSelection = probablyFriendsAfterSelection.TakeLast(7).ToList();
            MaybeFriendsSection = new ObservableCollection<MaybeFriendView>() {};
            foreach (User user in probablyFriendsAfterSelection)
            {
                MaybeFriendsSection.Add(new MaybeFriendView(new FriendDto() 
                { 
                    Id = user.Id,
                    Nickname = user.Nickname,
                    ProfilePhoto = db.ProfileImages.First(pi => pi.Id == user.Id)
                }, userId, LoadMaybeFriends) { });
            }
            if (MaybeFriendsSection.Count == 0)
            {
                MaybeFriendsMessage = "There is no new users :(";
            }
        }
        private void LoadFriendRequest(InstagramDbContext db, int userId)
        {
            FriendRequestSection = new ObservableCollection<FriendRequestView>() { };
            List<int> gotRequestPeopleIds = db.UserIdGotModels.Where(uigm => uigm.UserId == userId).Select(uigm => uigm.StoredUserId).ToList();
            foreach (int userThatSentRequestId in gotRequestPeopleIds)
            {
                FriendRequestSection.Add(new FriendRequestView(new FriendDto()
                {
                    Id = userThatSentRequestId,
                    ProfilePhoto = db.ProfileImages.First(pi => pi.UserId == userThatSentRequestId),
                    Nickname = db.Users.First(u => u.Id == userThatSentRequestId).Nickname,
                }, userId, LoadFriendRequest) { });
                if (FriendRequestSection.Count == 7)
                {
                    break;
                }
            }
            LoadMaybeFriends(db, _user.Id);
            if (FriendRequestSection.Count == 0)
            {
                FriendRequestMessage = "No friend requests :(";
            }
        }
    }
}
