using Instagram.Databases;
using Instagram.DTOs;
using Instagram.Interfaces;
using Instagram.Repositories;
using Instagram.StartupHelpers;
using Instagram.ViewModels;
using Instagram.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.ComponentsViewModels
{
    public class MessengerViewModel : ViewModelBase
    {
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

        private readonly IFriendRepository _friendRepository;
        private readonly IAbstractFactory<FriendInMessengerView> _friendFactory;

        public MessengerViewModel(InstagramDbContext db, IAbstractFactory<FriendInMessengerView> friendFactory)
        {
            _friendRepository = new FriendRepository(db);
            _friendFactory = friendFactory;
            Init();
        }

        public async Task Init()
        {
            FriendsList = new ObservableCollection<FriendInMessengerView>();
            List<int> friendsId = await _friendRepository.GetAllUserFriendsIdAsync(await GetUser.IdFromFile());
            foreach (int id in friendsId)
            {
                var friend = _friendFactory.Create();
                friend.SetDataContext(id, Init);
                FriendsList.Add(friend);
            }
        }
    }
}
