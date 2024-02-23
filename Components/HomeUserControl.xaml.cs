using Instagram.ComponentsViewModels;
using Instagram.Databases;
using Instagram.Interfaces;
using Instagram.JSONModels;
using Instagram.Models;
using Instagram.Repositories;
using Instagram.Services;
using System.Windows.Controls;

namespace Instagram.Components
{
    public partial class HomeUserControl : UserControl
    {
        public HomeUserControl(InstagramDbContext db)
        {
            ChangeHomeTheme();
            DataContext = new HomeViewModel(db);
            InitializeComponent();
        }

        public void ChangeHomeTheme()
        {
            ChangeTheme.Change(this.Resources);
        }
    }
}
