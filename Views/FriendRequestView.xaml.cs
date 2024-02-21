using Instagram.Databases;
using Instagram.DTOs;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Instagram.Views
{
    /// <summary>
    /// Interaction logic for FriendRequestView.xaml
    /// </summary>
    public partial class FriendRequestView : UserControl
    {
        public FriendRequestView(FriendDto friendDto, int userId, Action<int> LoadFriendRequest)
        {
            this.DataContext = new FriendRequestViewModel(friendDto, userId, LoadFriendRequest) { };
            InitializeComponent();
        }
    }
}
