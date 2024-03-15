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
using System.Windows.Media.Imaging;

namespace Instagram.ViewModels
{
    public class FriendViewModel : ViewModelBase
    {
        #region OnPropertyChangedProperties
        private BitmapImage _ProfilePhoto;
        public BitmapImage ProfilePhoto 
        { 
            get { return _ProfilePhoto; }
            set
            {
                _ProfilePhoto = value;
                OnPropertyChanged(nameof(ProfilePhoto));
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
        private string _LastMessage;
        public string LastMessage 
        {
            get { return _LastMessage; }
            set
            {
                _LastMessage = value;
                OnPropertyChanged(nameof(LastMessage));
            } 
        }
        private DateTime _LastMessageTime;
        public DateTime LastMessageTime 
        { 
            get { return _LastMessageTime; }
            set
            {
                _LastMessageTime = value;
                OnPropertyChanged(nameof(LastMessageTime));
            }
        }
        #endregion
        #region PrivateProperties
        private readonly IFriendRepository _friendRepository;
        private readonly IUserRepository _userRepository;
        private readonly int _friendId;
        #endregion

        public FriendViewModel(InstagramDbContext db, int friendId)
        {
            _friendRepository = new FriendRepository(db);
            _userRepository = new UserRepository(db);
            _friendId = friendId;
            Init();
        }

        private async Task Init()
        {
            Message message = await _friendRepository.GetLastMessageAsync(await _friendRepository.GetFriendId(await GetUser.IdFromFile(), _friendId));
            if (message != null)
            {
                LastMessage = message.Content;
                LastMessageTime = message.SendDate;
            }
            User user = await _userRepository.GetUserWithPhotoAndRequestsAsync(_friendId);
            ProfilePhoto = ConvertImage.FromByteArray(user.ProfilePhoto.ImageBytes);
            Nickname = user.Nickname;
        }
    }
}
