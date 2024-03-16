using Instagram.Databases;
using Instagram.DTOs;
using Instagram.Models;
using Instagram.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Instagram.Views
{
    public partial class FriendRequestView : UserControl
    {
        private readonly InstagramDbContext _db;

        public FriendRequestView(InstagramDbContext db)
        {
            InitializeComponent();
            _db = db;
        }

        public void SetDataContext(int requestFriendId, Func<Task> LoadFriendRequestAsync, Action<int> ShowCheckProfile)
        {
            this.DataContext = new FriendRequestViewModel(_db, requestFriendId, LoadFriendRequestAsync, ShowCheckProfile);
        }
    }
}
