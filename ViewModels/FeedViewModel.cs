using Instagram.Commands;
using Instagram.Components;
using Instagram.Databases;
using Instagram.DTOs;
using Instagram.JSONModels;
using Instagram.Models;
using Instagram.Services;
using Instagram.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private const string _HARDPATH = @"C:\Programs\Instagram\Instagram\Resources\";
        public BitmapImage ProfilePhotoSource { get; set; }
        public string LogoutPath { get; set; } = $"{_HARDPATH}logoutIcon.png";
        public string MessageIconPath { get; set; } = $"{_HARDPATH}messageIcon.png";
        public string HomeIconPath { get; set; } = $"{_HARDPATH}homeIcon.png";
        public string SearchIconPath { get; set; } = $"{_HARDPATH}searchIcon.png";
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
        public ICommand HomeButton { get; set; }
        public ICommand MessengerButton { get; set; }
        public ICommand SearchButton { get; set; }
        public ICommand ProfileButton { get; set; }
        public ICommand LogoutButton { get; set; }
        public ICommand CreateNewPost { get; set; }
        public ICommand DarkModeButton { get; set; }
        public ICommand SendEmailsButton { get; set; }
        private ObservableCollection<FriendRequestView> _FriendRequestSection;
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
        public ObservableCollection<StoryView> StoriesSection { get; set; }
        private User _user;
        private InstagramDbContext _db { get; set; }
        private StackPanel _feedViewMainContainer;
        private Action<bool> _ChangeLoginTheme;
        private Action<bool> _ChangeFeedTheme;
        public FeedViewModel(User user, Action CloseWindow, StackPanel feedViewMainContainer, Action<bool> ChangeLoginTheme, Action<bool> ChangeFeedTheme)
        {
            _user = user;
            _feedViewMainContainer = feedViewMainContainer;
            _ChangeLoginTheme = ChangeLoginTheme;
            _ChangeFeedTheme = ChangeFeedTheme;
            CreateNewPost = new CreateNewPostOpenWindowCommand(user, ShowPosts);
            LogoutButton = new LogoutCommand(CloseWindow);
            HomeButton = new ChangeMainContainerContentCommand(ShowPosts);
            MessengerButton = new ChangeMainContainerContentCommand(ShowMessenger);
            ProfileButton = new ChangeMainContainerContentCommand(ShowProfile);
            DarkModeButton = new DarkModeCommand(ChangeThemes);
            SendEmailsButton = new SendEmailsCommand();
            LoadThemeColourFromJsonFile();
            ShowPosts();
            LoadEverythingFromDatabase();
        }
        private void ChooseLogo(bool isDarkMode)
        {
            if (isDarkMode)
            {
                LogoPath = $"{_HARDPATH}/darkLogo.png";
            }
            else
            {
                LogoPath = $"{_HARDPATH}/logo.png";
            }
        }
        private void ChangeThemes(bool isDarkMode)
        {
            _ChangeLoginTheme.Invoke(isDarkMode);
            _ChangeFeedTheme.Invoke(isDarkMode);
            ChooseLogo(isDarkMode);
        }
        private void LoadThemeColourFromJsonFile()
        {
            JSON<UserDataModel> userJSON = new JSON<UserDataModel>("UserData");
            UserDataModel userJSONModel = userJSON.Get<UserDataModel>();
            ChangeThemes(userJSONModel.DarkMode);
            IsDarkModeOn = userJSONModel.DarkMode;
        }
        private void LoadEverythingFromDatabase()
        {
            using (_db = new InstagramDbContext())
            {
                LoadProfilePhoto();
                ShowStories();
                LoadFriendRequest(_db, _user.Id);
            }
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
        private void ShowStories()
        {

        }
        private void ShowPosts()
        {
            HomeUserControl homeUserControl = new HomeUserControl(_user.Id);
            ShowSomethingInMainBox(homeUserControl);
        }
        private void ShowProfile()
        {
            ProfileUserControl profileUserControl = new ProfileUserControl();
            ShowSomethingInMainBox(profileUserControl);
        }
        private void ShowMessenger()
        {
            MessengerUserControl messengerUserControl = new MessengerUserControl(_user.Id);
            ShowSomethingInMainBox(messengerUserControl);
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
            // if less 0 maybe friends show message
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
            }
            LoadMaybeFriends(db, _user.Id);
            // if no friend requests show message
            // if over 7 add scrollbar
        }
    }
}
