using Instagram.Databases;
using Instagram.DTOs;
using Instagram.JSONModels;
using Instagram.Models;
using Instagram.Services;
using Instagram.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Instagram.Components
{
    /// <summary>
    /// Interaction logic for MessengerUserControl.xaml
    /// </summary>
    public partial class MessengerUserControl : UserControl
    {
        public static DependencyProperty MessengerSourceProperty = DependencyProperty.Register("MessengerSource", typeof(ObservableCollection<FriendInMessengerView>), typeof(MessengerUserControl), new PropertyMetadata(new ObservableCollection<FriendInMessengerView>()));
        public ObservableCollection<FriendInMessengerView> MessengerSource
        {
            get { return (ObservableCollection<FriendInMessengerView>)GetValue(MessengerSourceProperty); }
            set { SetValue(MessengerSourceProperty, value); }
        }
        private ObservableCollection<FriendDto> _friendsFromDb;
        private int _userId;
        public MessengerUserControl(int userId)
        {
            _userId = userId;
            InitializeComponent();
            GetUsersFromDbAsync();
            CreateFriendsInMessenger();
        }
        private async Task GetUsersFromDbAsync()
        {
            var GetUsersFromDatabaseAsync = async Task () =>
            {
                //using (var db = new InstagramDbContext("MainDb"))
                //{
                //    _friendsFromDb = new ObservableCollection<FriendDto>();
                //    List<Friend> friends = db.Friends.Where(f => f.UserId == _userId).ToList();
                //    foreach (Friend friend in friends)
                //    {
                //        _friendsFromDb.Add(new FriendDto()
                //        {
                //            Id = friend.FriendId,
                //            Nickname = db.Users.First(u => u.Id == friend.FriendId).Nickname,
                //            ProfilePhoto = db.ProfileImages.First(pi => pi.UserId == friend.FriendId),
                //            LastMessage = "not done yet :("
                //        });
                //    }
                //}
            };
            await GetUsersFromDatabaseAsync.Invoke();
        }
        private void CreateFriendsInMessenger()
        {
            ObservableCollection<FriendInMessengerView> friendsModels = new ObservableCollection<FriendInMessengerView>();
            foreach (FriendDto friend in _friendsFromDb)
            {
                friendsModels.Add(new FriendInMessengerView(friend) {});
            }
            MessengerSource = friendsModels;
        }
        public void ChangeMessengerTheme(bool isDarkMode)
        {
            this.Resources.MergedDictionaries.Clear();
            string resourceName = isDarkMode ? "DarkModeDictionary" : "BrightModeDictionary";
            ResourceDictionary resourceDictionary = new ResourceDictionary() { Source = new Uri(string.Format("ResourceDictionaries/{0}.xaml", resourceName), UriKind.Relative) };
            this.Resources.MergedDictionaries.Add(resourceDictionary);
        }
    }
}
