using Instagram.Commands;
using Instagram.Databases;
using Instagram.Interfaces;
using Instagram.JSONModels;
using Instagram.Models;
using Instagram.Repositories;
using Instagram.Services;
using Instagram.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Instagram.ComponentsViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        #region OnPropertyChangedProperties
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
        private DateTime _Birthdate;
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
        #endregion
        #region PrivateProperties
        private readonly string _path;
        private readonly IUserRepository _userRepository;
        private User _user;
        #endregion
        #region Commands
        public ICommand UpdateAccountButton { get; set; }
        public ICommand OpenImageButton { get; set; }
        #endregion

        public ProfileViewModel(InstagramDbContext db)
        {
            _userRepository = new UserRepository(db);
            UpdateAccountButton = new UpdateAccountCommand(_userRepository, UserObject);
            OpenImageButton = new OpenImageButtonCommand(OnLoadingImage);
            InitResourcesAsync();
        }

        private User UserObject()
        {
            _user.ProfilePhoto.ImageBytes = ConvertImage.ImageToBytaArray(ProfilePhotoSource);
            if (FirstPassword != string.Empty)
            {
                _user.Password = Hash.HashString(FirstPassword);
            }
            _user.Nickname = Nickname;
            _user.EmailAdress = Email;
            _user.FirstName = FirstName;
            _user.LastName = LastName;
            _user.Birthdate = Birthdate;
            return _user;
        }

        private async Task InitResourcesAsync()
        {
            _user = await _userRepository.GetUserWithPhotoAndRequestsAsync(await GetUser.IdFromFile());
            ProfilePhotoSource = ConvertImage.FromByteArray(_user.ProfilePhoto.ImageBytes);
            Nickname = _user.Nickname;
            Email = _user.EmailAdress;
            FirstName = _user.FirstName;
            LastName = _user.LastName;
            Birthdate = _user.Birthdate;
        }

        private void OnLoadingImage(string imagePath)
        {
            ProfilePhotoSource = ConvertImage.FromByteArray(ConvertImage.ToByteArray(imagePath));
            OpenImageButtonContent = "PHOTO LOADED";
        }
    }
}
