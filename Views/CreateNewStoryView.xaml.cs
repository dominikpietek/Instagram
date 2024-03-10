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
        public CreateNewStoryView(InstagramDbContext db)
        {
            InitializeComponent();
            ChangeTheme.ChangeAsync(this.Resources);
            this.DataContext = new CreateNewStoryViewModel(db, CloseWindow);
        }
        public void CloseWindow()
        {
            this.Close();
        }
    }
}
