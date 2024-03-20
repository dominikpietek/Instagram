using Instagram.Databases;
using Instagram.DTOs;
using Instagram.Models;
using Instagram.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Instagram.Views
{
    public partial class FriendInMessengerView : UserControl
    {
        private readonly InstagramDbContext _db;

        public FriendInMessengerView(InstagramDbContext db)
        {
            InitializeComponent();
            _db = db;
        }

        public void SetDataContext(int friendId, Func<Task> UpdateMessenger)
        {
            DataContext = new FriendInMessengerViewModel(_db, friendId, UpdateMessenger);
        }
    }
}
