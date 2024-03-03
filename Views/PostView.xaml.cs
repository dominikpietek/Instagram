using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Instagram.Databases;
using Instagram.Models;
using Instagram.StartupHelpers;
using Instagram.ViewModels;

namespace Instagram.Views
{
    public partial class PostView : UserControl
    {
        private readonly InstagramDbContext _db;
        private readonly IAbstractFactory<CommentView> _commentFactory;
        public PostView(InstagramDbContext db, IAbstractFactory<CommentView> commentFactory)
        {
            InitializeComponent();
            _db = db;
            _commentFactory = commentFactory;
        }
        public void AddDataContext(int id, Action ChangeHomeTheme)
        {
            DataContext = new PostViewModel(_db, _commentFactory, id, ChangeHomeTheme);
        }
    }
}
