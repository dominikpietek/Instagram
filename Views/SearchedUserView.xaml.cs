using Instagram.Databases;
using Instagram.ViewModels;
using System;
using System.Windows.Controls;

namespace Instagram.Views
{
    public partial class SearchedUserView : UserControl
    {
        private readonly InstagramDbContext _db;

        public SearchedUserView(InstagramDbContext db)
        {
            InitializeComponent();
            _db = db;
        }

        public void SetDataContext(int userId, Action<int> ShowCheckProfile)
        {
            this.DataContext = new SearchedUserViewModel(_db, userId, ShowCheckProfile);
        }
    }
}
