using Instagram.Commands;
using Instagram.Components;
using Instagram.Databases;
using Instagram.DTOs;
using Instagram.Interfaces;
using Instagram.Models;
using Instagram.Repositories;
using Instagram.StartupHelpers;
using Instagram.ViewModels;
using Instagram.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Instagram.ComponentsViewModels
{
    public class MessengerViewModel : ViewModelBase
    {
        #region ResourcesProperties
        private string _path;
        public string SendIcon { get; set; }
        #endregion
        #region OnPropertyChangedProperties
        private string _WritenTextInMessenger;
        public string WritenTextInMessenger 
        {
            get { return _WritenTextInMessenger; }
            set
            {
                _WritenTextInMessenger = value;
                OnPropertyChanged(nameof(WritenTextInMessenger));
            }
        }
        private bool _AreAnyFriendsInMessenger;
        public bool AreAnyFriendsInMessenger 
        {
            get { return _AreAnyFriendsInMessenger; }
            set
            {
                _AreAnyFriendsInMessenger = value;
                OnPropertyChanged(nameof(AreAnyFriendsInMessenger));
            } 
        }
        private bool _AreMessagesShown;
        public bool AreMessagesShown
        {
            get { return _AreMessagesShown; }
            set
            {
                _AreMessagesShown = value;
                OnPropertyChanged(nameof(AreMessagesShown));
            }
        }
        private ObservableCollection<FriendInMessengerView> _FriendsList = new ObservableCollection<FriendInMessengerView>();
        public ObservableCollection<FriendInMessengerView> FriendsList 
        { 
            get { return _FriendsList; }
            set
            {
                _FriendsList = value;
                OnPropertyChanged(nameof(FriendsList));
            }
        }

        private ObservableCollection<MessageView> _Message = new ObservableCollection<MessageView>();
        public ObservableCollection<MessageView> Message
        {
            get { return _Message; }
            set
            {
                _Message = value;
                OnPropertyChanged(nameof(Message));
            }
        }
        #endregion
        #region Commands
        public ICommand SendMessageButton { get; set; }
        #endregion
        #region PrivateProperties
        private readonly IFriendRepository _friendRepository;
        private readonly IAbstractFactory<FriendInMessengerView> _friendFactory;
        private readonly IMessageRepository _messageRepository;
        private readonly Action _ScrollToBottom;
        private int _userId;
        private int _friendId = 0;
        #endregion
        public MessengerViewModel(InstagramDbContext db, IAbstractFactory<FriendInMessengerView> friendFactory, Action ScrollToBottom)
        {
            #region ProperitesAssignement
            _path = ConfigurationManager.AppSettings["ResourcesPath"]!;
            _friendRepository = new FriendRepository(db);
            _friendFactory = friendFactory;
            _messageRepository = new MessageRepository(db);
            _ScrollToBottom = ScrollToBottom;
            AreMessagesShown = false;
            SendMessageButton = new SendMessageCommand(SendMessage);
            #endregion
            Init();
        }

        public async Task Init()
        {
            SendIcon = $"{_path}/sendMessageIcon.png";
            _userId = await GetUser.IdFromFile(); ;
            AreMessagesShown = false;
            FriendsList = new ObservableCollection<FriendInMessengerView>();
            List<int> friendsId = await _friendRepository.GetAllUserFriendsIdAsync(await GetUser.IdFromFile());
            foreach (int id in friendsId)
            {
                var friend = _friendFactory.Create();
                friend.SetDataContext(id, Init, ShowMessages);
                FriendsList.Add(friend);
            }
            if (FriendsList.Count() == 0) AreAnyFriendsInMessenger = false;
            else AreAnyFriendsInMessenger = true;
        }

        public async Task ShowMessages(int friendId)
        {
            _friendId = friendId;
            List<Message> myMessages = await _messageRepository.GetUserMessagesToFriend(_userId, friendId);
            List<Message> hisMessages = await _messageRepository.GetUserMessagesToFriend(friendId, _userId);
            List<Message> messages = myMessages.Concat(hisMessages).OrderBy(m => m.SendDate).ToList();
            int previousMessageId = _userId;
            foreach (Message message in messages)
            {
                if (previousMessageId != message.UserId)
                {
                    Message.Add(new MessageView(message, true));
                }
                Message.Add(new MessageView(message, false));
                previousMessageId = message.UserId;
            }
            AreMessagesShown = true;
            _ScrollToBottom.Invoke();
        }

        public async Task SendMessage()
        {
            Message message = new Message()
            {
                Content = WritenTextInMessenger,
                UserId = _userId,
                FriendId = _friendId,
                SendDate = DateTime.Now
            };
            await _messageRepository.AddMessage(message);
            WritenTextInMessenger = "";
            Message.Add(new MessageView(message, true));
        }
    }
}
