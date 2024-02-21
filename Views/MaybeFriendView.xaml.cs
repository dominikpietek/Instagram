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
    /// Interaction logic for MaybeFriendView.xaml
    /// </summary>
    public partial class MaybeFriendView : UserControl
    {
        public MaybeFriendView(FriendDto friendDto, int userId, Action<int> LoadMaybeFriends)
        {
            DataContext = new MaybeFriendViewModel(friendDto, userId, LoadMaybeFriends);
            InitializeComponent();
        }
    }
}
