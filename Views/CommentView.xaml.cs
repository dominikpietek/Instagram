using Instagram.Databases;
using Instagram.Models;
using Instagram.Services;
using Instagram;
using Instagram.StartupHelpers;
using Instagram.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Instagram.Views
{
    public partial class CommentView : UserControl
    {
        private readonly InstagramDbContext _db;
        private readonly IAbstractFactory<ReplyCommentView> _replyCommentFactory;
        public int Id { get; set; }

        public CommentView(InstagramDbContext db, IAbstractFactory<ReplyCommentView> replyCommentFactory)
        {
            InitializeComponent();
            _db = db;
            _replyCommentFactory = replyCommentFactory;
        }
        public void AddDataContext(int id, Func<Task> ChangeHomeTheme, Action<int> UpdateCommentsAfterDelete)
        {
            Id = id;
            DataContext = new CommentViewModel(_db, _replyCommentFactory, id, ChangeHomeTheme, UpdateCommentsAfterDelete);
        }
    }
}
