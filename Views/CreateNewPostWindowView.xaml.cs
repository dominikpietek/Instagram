using Instagram.Databases;
using Instagram.Services;
using Instagram.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace Instagram.Views
{
    public partial class CreateNewPostWindowView : Window
    {
        private Func<Task> _UpdatePosts;
        public CreateNewPostWindowView(InstagramDbContext db)
        {
            InitializeComponent();
            ChangeTheme.ChangeAsync(this.Resources);
            DataContext = new CreatePostViewModel(CloseWindow, db);
        }

        public void CloseWindow()
        {
            this.Close();
            _UpdatePosts.Invoke();
        }

        public void InitData(Func<Task> UpdatePosts)
        {
            _UpdatePosts = UpdatePosts;
        }
    }
}
