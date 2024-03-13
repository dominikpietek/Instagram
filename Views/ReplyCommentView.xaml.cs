using Instagram.Databases;
using Instagram.Models;
using Instagram.ViewModels;
using System;
using System.Windows.Controls;

namespace Instagram.Views
{
    public partial class ReplyCommentView : UserControl
    {
        private readonly InstagramDbContext _db;
        public int Id { get; set; }

        public ReplyCommentView(InstagramDbContext db)
        {
            InitializeComponent();
            _db = db;
        }
        public void AddDataContext(int id, Action<int> UpdateCommentsResponseAfterDelete)
        {
            Id = id;
            DataContext = new ReplyCommentViewModel(_db, id, UpdateCommentsResponseAfterDelete);
        }
    }
}
