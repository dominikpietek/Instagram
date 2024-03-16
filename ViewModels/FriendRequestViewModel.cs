using Instagram.Commands;
using Instagram.Databases;
using Instagram.DTOs;
using Instagram.Interfaces;
using Instagram.Models;
using Instagram.Repositories;
using Instagram.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Instagram.ViewModels
{
    public class FriendRequestViewModel : ViewModelBase
    {
        #region Resources
        private string _path;
        public BitmapImage ProfilePhoto { get; set; }
        public string DeclineIconSource { get; set; }
        public string AcceptIconSource { get; set; }
        public string Nickname { get; set; }
        #endregion
        #region Commands
        public ICommand CheckProfileButton { get; set; }
        public ICommand AcceptRequestButton { get; set; }
        public ICommand DeclineRequestButton { get; set; }
        #endregion
        #region PrivateProperties
        private int _userId;
        private int _friendRequestId;
        private readonly IFriendRepository _friendRepository;
        private readonly IUserRepository _userRepository;
        private readonly IGotSentFriendRequestModelRepository _gotRepository;
        private readonly IGotSentFriendRequestModelRepository _sentRepository;
        private readonly Func<Task> _LoadFriendRequestAsync;
        #endregion
        public FriendRequestViewModel(InstagramDbContext db, int friendRequestId, Func<Task> LoadFriendRequestAsync, Action<int> ShowCheckProfile)
        {
            #region CommandsInstances
            CheckProfileButton = new ShowProfileCommand(ShowCheckProfile, friendRequestId);
            AcceptRequestButton = new AcceptRequestCommand(AddFriendAsync);
            DeclineRequestButton = new DeclineRequestCommand(RemoveFriendAsync);
            #endregion
            #region PrivatePropertiesAssignment
            _path = ConfigurationManager.AppSettings.Get("ResourcesPath")!;
            _friendRequestId = friendRequestId;
            _friendRepository = new FriendRepository(db);
            _userRepository = new UserRepository(db);
            _gotRepository = new GotSentFriendRequestModelRepository<GotFriendRequestModel>(db);
            _sentRepository = new GotSentFriendRequestModelRepository<SentFriendRequestModel>(db);
            _LoadFriendRequestAsync = LoadFriendRequestAsync;
            #endregion
            InitAsync();
        }
        private async Task InitAsync()
        {   
            DeclineIconSource = $"{_path}declineIcon.png";
            AcceptIconSource = $"{_path}acceptIcon.png";
            User user = await _userRepository.GetUserWithPhotoAndRequestsAsync(_friendRequestId);
            ProfilePhoto = ConvertImage.FromByteArray(user.ProfilePhoto.ImageBytes);
            Nickname = user.Nickname;
            _userId = await GetUser.IdFromFile();
        }

        private async Task AddFriendAsync()
        {
            await _friendRepository.AddFriendAsync(_userId, _friendRequestId);
            await _friendRepository.AddFriendAsync(_friendRequestId, _userId);
            await RemoveFriendAsync();
        }

        private async Task RemoveFriendAsync()
        {
            await _gotRepository.RemoveAsync(_userId, _friendRequestId);
            await _sentRepository.RemoveAsync(_userId, _friendRequestId);
            _LoadFriendRequestAsync.Invoke();
        }
    }
}
