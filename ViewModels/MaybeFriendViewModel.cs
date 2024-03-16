using Instagram.Commands;
using Instagram.Databases;
using Instagram.DTOs;
using Instagram.Interfaces;
using Instagram.Models;
using Instagram.Repositories;
using Instagram.Services;
using Microsoft.EntityFrameworkCore.Metadata;
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
    public class MaybeFriendViewModel : ViewModelBase
    {
        #region Resources
        private string _path;
        public BitmapImage ProfilePhoto { get; set; }
        public string AddUserIconPath { get; set; }
        public string Nickname { get; set; }
        #endregion
        #region Commands
        public ICommand CheckProfileButton { get; set; }
        public ICommand AddUserButton { get; set; }
        #endregion
        #region OnPropertyChangeProperties
        private bool _IsInvitationSent = false;
        public bool IsInvitationSent
        {
            get { return _IsInvitationSent; }
            set
            {
                _IsInvitationSent = value;
                OnPropertyChanged(nameof(IsInvitationSent));
            }
        }
        #endregion
        #region PrivateProperties
        private readonly IGotSentFriendRequestModelRepository _gotRepository;
        private readonly IGotSentFriendRequestModelRepository _sentRepository;
        private readonly IUserRepository _userRepository;
        private readonly int _friendId;
        private User _user;
        private int _userId;
        #endregion
        public MaybeFriendViewModel(InstagramDbContext db, int friendId, Action<int> ShowCheckProfile)
        {
            #region CommandInstances
            CheckProfileButton = new ShowProfileCommand(ShowCheckProfile, friendId);
            AddUserButton = new SendInvitationCommand(ChangeInvitationStatus);
            #endregion
            #region PrivatePropertiesAssignement
            _path = ConfigurationManager.AppSettings.Get("ResourcesPath")!;
            _gotRepository = new GotSentFriendRequestModelRepository<GotFriendRequestModel>(db);
            _sentRepository = new GotSentFriendRequestModelRepository<SentFriendRequestModel>(db);
            _userRepository = new UserRepository(db);
            _friendId = friendId;
            #endregion
            InitResources();
        }

        private async Task InitResources()
        {
            AddUserIconPath = $"{_path}plusIcon.png";
            _user = await _userRepository.GetUserWithPhotoAndRequestsAsync(_friendId);
            ProfilePhoto = ConvertImage.FromByteArray(_user.ProfilePhoto.ImageBytes);
            Nickname = _user.Nickname;
            _userId = await GetUser.IdFromFile();
        }

        private async Task ChangeInvitationStatus()
        {
            IsInvitationSent ^= true;
            if (IsInvitationSent)
            {
                await SentInvitationAsync();
            }
            else
            {
                await UnSentInvitationAsync();
            }
            
        }
        private async Task SentInvitationAsync()
        {
            await _gotRepository.AddAsync(new GotFriendRequestModel() { StoredUserId = _userId, UserId = _friendId });
            await _sentRepository.AddAsync(new SentFriendRequestModel() { StoredUserId = _friendId, UserId = _userId });
        }

        private async Task UnSentInvitationAsync()
        {
            await _gotRepository.RemoveAsync(_userId, _friendId);
            await _sentRepository.RemoveAsync(_userId, _friendId);
        }
    }
}
