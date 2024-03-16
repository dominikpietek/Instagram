using Instagram.Commands;
using Instagram.Databases;
using Instagram.Interfaces;
using Instagram.Models;
using Instagram.Repositories;
using Instagram.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Instagram.ViewModels
{
    public class SearchedUserViewModel : ViewModelBase
    {
        public BitmapImage ProfilePhotoSource { get; set; }
        public string Nickname { get; set; }

        private readonly IUserRepository _userRepository;
        private readonly int _userId;

        public ICommand CheckProfileButton { get; set; }

        public SearchedUserViewModel(InstagramDbContext db, int userId, Action<int> ShowCheckProfile)
        {
            _userRepository = new UserRepository(db);
            _userId = userId;
            CheckProfileButton = new ShowProfileCommand(ShowCheckProfile, userId);
            InitAsync();
        }

        private async Task InitAsync()
        {
            User user = await _userRepository.GetUserWithPhotoAndRequestsAsync(_userId);
            ProfilePhotoSource = ConvertImage.FromByteArray(user.ProfilePhoto.ImageBytes);
            Nickname = user.Nickname;
        }
    }
}
