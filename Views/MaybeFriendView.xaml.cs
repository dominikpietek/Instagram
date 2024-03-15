using Instagram.Databases;
using Instagram.DTOs;
using Instagram.ViewModels;
using System;
using System.Windows.Controls;

namespace Instagram.Views
{
    public partial class MaybeFriendView : UserControl
    {
        private readonly InstagramDbContext _db;

        public MaybeFriendView(InstagramDbContext db)
        {
            InitializeComponent();
            _db = db;
        }

        public void SetDataContext(int friendId)
        {
            DataContext = new MaybeFriendViewModel(_db, friendId);
        }
    }
}
