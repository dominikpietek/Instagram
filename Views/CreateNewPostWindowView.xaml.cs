using Instagram.Databases;
using Instagram.Services;
using Instagram.ViewModels;
using System.Windows;

namespace Instagram.Views
{
    public partial class CreateNewPostWindowView : Window
    {
        public CreateNewPostWindowView(InstagramDbContext db)
        {
            InitializeComponent();
            ChangeTheme.ChangeAsync(this.Resources);
            DataContext = new CreatePostViewModel(CloseWindow, db);
        }
        public void CloseWindow()
        {
            // show posts recall
            this.Close();
        }
    }
}
