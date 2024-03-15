using Instagram.Databases;
using Instagram.ViewModels;
using System.Windows.Controls;

namespace Instagram.Views
{
    public partial class FriendView : UserControl
    {
        private readonly InstagramDbContext _db;

        public FriendView(InstagramDbContext db)
        {
            InitializeComponent();
            _db = db;
        }

        public void SetDataContext(int friendId)
        {
            this.DataContext = new FriendViewModel(_db, friendId);
        }
    }
}
