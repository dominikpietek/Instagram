using Instagram.Commands;
using Instagram.Components;
using Instagram.Databases;
using Instagram.DTOs;
using Instagram.Interfaces;
using Instagram.Models;
using Instagram.Repositories;
using Instagram.Services;
using Instagram.StartupHelpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Instagram.ViewModels
{
    public class FriendInMessengerViewModel : ViewModelBase
    {
        #region Resources
        private string _path;
        public string TrashIconPath { get; set; }
        public BitmapImage ProfilePhotoSource { get; set; }
        #endregion
        #region Properties
        public string Nickname { get; set; }
        public string LastMessage { get; set; }
        #endregion
        #region Commands
        public ICommand OpenChatButton { get; set; }
        public ICommand RemoveFriendButton { get; set; }
        #endregion
        #region PrivateProperties
        private int _friendId;
        private readonly IFriendRepository _friendRepository;
        private readonly IUserRepository _userRepository;
        private readonly Func<Task> _UpdateMessenger;
        #endregion
        public FriendInMessengerViewModel(InstagramDbContext db, int friendId, Func<Task> UpdateMessenger, Func<int, Task> ShowMessages)
        {
            #region PrivatePropertiesAssignement
            _friendRepository = new FriendRepository(db);
            _userRepository = new UserRepository(db);
            _UpdateMessenger = UpdateMessenger;
            _friendId = friendId;
            _path = ConfigurationManager.AppSettings.Get("ResourcesPath")!;
            #endregion
            #region CommandsInstances
            OpenChatButton = new OpenChatCommand(ShowMessages, friendId);
            RemoveFriendButton = new RemoveFriendCommand(RemoveFriend);
            #endregion
            InitResources();
            InitAsync();
        }

        private void InitResources()
        {
            TrashIconPath = $"{_path}trashIcon.png";
        }

        private async Task InitAsync()
        {
            User user = await _userRepository.GetUserWithPhotoAndRequestsAsync(_friendId);
            Nickname = user.Nickname;
            //LastMessage = (await _friendRepository.GetLastMessageAsync(_friendId)).Content;
            ProfilePhotoSource = ConvertImage.FromByteArray(user.ProfilePhoto.ImageBytes);
        }

        public async Task RemoveFriend()
        {
            await _friendRepository.RemoveFriendAsync(await GetUser.IdFromFile(), _friendId);
            await _UpdateMessenger.Invoke();
        }
    }
}
