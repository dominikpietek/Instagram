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
using Instagram.Databases;
using Instagram.Models;
using Instagram.ViewModels;

namespace Instagram.Views
{
    /// <summary>
    /// Interaction logic for Post.xaml
    /// </summary>
    public partial class PostView : UserControl
    {
        public PostView(Post post, int actualUserId, Action ShowPosts)
        {
            InitializeComponent();
            DataContext = new PostViewModel(post, actualUserId, ShowPosts);
        }
    }
}
