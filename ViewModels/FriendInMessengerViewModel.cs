using Instagram.Commands;
using Instagram.Databases;
using Instagram.Interfaces;
using Instagram.Models;
using Instagram.Repositories;
using Instagram.Services;
using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Instagram.ViewModels
{
    public class FriendInMessengerViewModel : ViewModelBase
    {
        #region Resources
        private string _path;
        public string TrashIconPath { get; set; }
        private BitmapImage _ProfilePhotoSource;
        public BitmapImage ProfilePhotoSource 
        {
            get {  return _ProfilePhotoSource; }
            set
            {
                _ProfilePhotoSource = value;
                OnPropertyChanged(nameof(ProfilePhotoSource));
            }
        }
        #endregion
        #region Properties
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
        private readonly IMessageRepository _messageRepository;
        #endregion
        public FriendInMessengerViewModel(InstagramDbContext db, int friendId, Func<Task> UpdateMessenger, Func<int, Task> ShowMessages)
        {
            #region PrivatePropertiesAssignement
            _friendRepository = new FriendRepository(db);
            _userRepository = new UserRepository(db);
            _messageRepository = new MessageRepository(db);
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
            LastMessage = await CreateLastMessage(user.Nickname);
            ProfilePhotoSource = ConvertImage.FromByteArray(user.ProfilePhoto.ImageBytes);
        }

        private async Task<string> CreateLastMessage(string userName)
        {
            int userId = await GetUser.IdFromFile();
            Message myLast = (await _messageRepository.GetUserMessagesToFriend(userId, _friendId)).Last();
            Message hisLast = (await _messageRepository.GetUserMessagesToFriend(_friendId, userId)).Last();
            if (myLast.SendDate > hisLast.SendDate) return $"you: {myLast.Content}";
            else return $"{userName}: {hisLast.Content}";
        }

        public async Task RemoveFriend()
        {
            await _friendRepository.RemoveFriendAsync(await GetUser.IdFromFile(), _friendId);
            await _UpdateMessenger.Invoke();
        }
    }
}
