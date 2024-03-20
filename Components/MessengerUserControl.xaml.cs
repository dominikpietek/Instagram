using Instagram.ComponentsViewModels;
using Instagram.Databases;
using Instagram.DTOs;
using Instagram.JSONModels;
using Instagram.Models;
using Instagram.Services;
using Instagram.StartupHelpers;
using Instagram.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Instagram.Components
{
    public partial class MessengerUserControl : UserControl
    {
        public MessengerUserControl(InstagramDbContext db, IAbstractFactory<FriendInMessengerView> friendFactory)
        {
            ChangeMessengerTheme();
            this.DataContext = new MessengerViewModel(db, friendFactory);
            InitializeComponent();
        }

        public async Task ChangeMessengerTheme()
        {
            await ChangeTheme.ChangeAsync(this.Resources);
        }
    }
}
