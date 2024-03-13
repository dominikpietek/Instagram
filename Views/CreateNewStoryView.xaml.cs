using Instagram.Databases;
using Instagram.Services;
using Instagram.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Instagram.Views
{
    public partial class CreateNewStoryView : Window
    {
        private readonly InstagramDbContext _db;

        public CreateNewStoryView(InstagramDbContext db)
        {
            InitializeComponent();
            ChangeTheme.ChangeAsync(this.Resources);
            _db = db;
        }
        public void CloseWindow()
        {
            this.Close();
        }
        
        public void SetDataContext(Func<bool, Task> UpdatePosts)
        {
            this.DataContext = new CreateNewStoryViewModel(_db, CloseWindow, UpdatePosts);
        }
    }
}
