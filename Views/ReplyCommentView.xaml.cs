using Instagram.Databases;
using Instagram.Models;
using Instagram.ViewModels;
using System.Windows.Controls;

namespace Instagram.Views
{
    public partial class ReplyCommentView : UserControl
    {
        private readonly InstagramDbContext _db;

        public ReplyCommentView(InstagramDbContext db)
        {
            InitializeComponent();
            _db = db;
        }
        public void AddDataContext(int id)
        {
            DataContext = new ReplyCommentViewModel(_db, id);
        }
    }
}
