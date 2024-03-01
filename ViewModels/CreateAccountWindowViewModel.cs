using Instagram.Commands;
using Instagram.Databases;
using Instagram.Interfaces;
using Instagram.JSONModels;
using Instagram.Models;
using Instagram.Repositories;
using Instagram.Services;
using Instagram.StartupHelpers;
using Instagram.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Instagram.ViewModels
{
    public class CreateAccountWindowViewModel : ViewModelBase
    {
        #region OnPropertyChangeProperties
        private string _BackgroundColour;
        public string BackgroundColour
        {
            get { return _BackgroundColour; }
            set
            {
                _BackgroundColour = value;
                OnPropertyChanged(nameof(BackgroundColour));
            }
        }
        private string _Nickname;
        public string Nickname 
        {
            get { return _Nickname; }
            set 
            {
                _Nickname = value;
                OnPropertyChanged(nameof(Nickname));
            } 
        }
        private string _Email;
        public string Email
        {
            get { return _Email; }
            set
            {
                _Email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        private string _FirstPassword;
        public string FirstPassword
        {
            get { return _FirstPassword; }
            set
            {
                _FirstPassword = value;
                OnPropertyChanged(nameof(FirstPassword));
            }
        }
        private string _SecondPassword;
        public string SecondPassword
        {
            get { return _SecondPassword; }
            set
            {
                _SecondPassword = value;
                OnPropertyChanged(nameof(SecondPassword));
            }
        }
        private string _FirstName;
        public string FirstName
        {
            get { return _FirstName; }
            set
            {
                _FirstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
        private string _LastName;
        public string LastName
        {
            get { return _LastName; }
            set
            {
                _LastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
        private DateTime _Birthdate = DateTime.Now;
        public DateTime Birthdate 
        { 
            get { return _Birthdate; }
            set
            {
                _Birthdate = value;
                OnPropertyChanged(nameof(Birthdate));
            }
        }
        private string _OpenImageButtonContent = "IMPORT PHOTO";
        public string OpenImageButtonContent
        {
            get { return _OpenImageButtonContent; }
            set
            {
                _OpenImageButtonContent = value;
                OnPropertyChanged(nameof(OpenImageButtonContent));
            }
        }
        #endregion
        #region PrivateProperties
        private readonly string _path;
        private string _ProfilePhotoSource { get; set; }
        private Action _CloseWindow;
        private Action<bool> _ChangeTheme;
        private readonly RegisterRepository _registerRepository;
        private readonly IAbstractFactory<LoginOrRegisterWindowView> _factory;
        #endregion
        #region Commands
        public ICommand ReturnToLoginPageButton { get; set; }
        public ICommand OpenImageButton { get; set; }
        public ICommand CreateAccountButton { get; set; }
        #endregion
        public CreateAccountWindowViewModel(
            Action CloseWindow, 
            Action<bool> ChangeTheme,  
            InstagramDbContext db,
            IAbstractFactory<LoginOrRegisterWindowView> factory)
        {
            #region PrivetPropertiesAssignement
            _path = ConfigurationManager.AppSettings.Get("ResourcesPath");
            _ChangeTheme = ChangeTheme;
            _registerRepository = new RegisterRepository(db);
            _factory = factory;
            _CloseWindow = CloseWindow;
            #endregion
            InitResourcesAsync();
            #region CommandsInstances
            ReturnToLoginPageButton = new ReturnToLoginPageButtonCommand(CloseWindowAndOpenLoginWindow);
            OpenImageButton = new OpenImageButtonCommand(OnLoadingImage);
            CreateAccountButton = new CreateAccountCommand(AddingUserToDatabase, CloseWindowAndOpenLoginWindow);
            #endregion
        }
        private async Task InitResourcesAsync()
        {
            _ProfilePhotoSource = $"{_path}defaultProfilePhoto.png";
            JSON<UserDataModel> userJSON = new JSON<UserDataModel>("UserData");
            bool isDarkMode = await userJSON.GetDarkModeAsync();
            BackgroundColour = isDarkMode ? "#CBC8CC" : "white";
            _ChangeTheme(isDarkMode);
        }
        private void OnLoadingImage(string imagePath)
        {
            _ProfilePhotoSource = imagePath;
            OpenImageButtonContent = "PHOTO LOADED";
        }
        public void CloseWindowAndOpenLoginWindow()
        {
            _factory.Create().Show();
            _CloseWindow.Invoke();
        }
        private User CreateNewUser()
        {
            ProfileImage profilePhoto = new ProfileImage()
            {
                ImageBytes = ConvertImage.ToByteArray(_ProfilePhotoSource)
            };
            User newUser = new User()
            {
                Nickname = _Nickname,
                EmailAdress = _Email,
                Password = Hash.HashString(_FirstPassword),
                FirstName = _FirstName,
                LastName = _LastName,
                Birthdate = _Birthdate,
                ProfilePhoto = profilePhoto
            };
            return newUser;
        }
        private async Task<bool> AddingUserToDatabase()
        {
            if (await _registerRepository.ValidateData(_FirstPassword, _SecondPassword, _Email, _Nickname))
            {
                User newUser = CreateNewUser();
                try
                {
                    await _registerRepository.AddUserAsync(newUser);
                    MessageBox.Show("Account created succesfully!");
                    return true;
                }
                catch (Exception)
                {
                    MessageBox.Show(
                        "Can't create account!", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            return false;
        }
    }
}
