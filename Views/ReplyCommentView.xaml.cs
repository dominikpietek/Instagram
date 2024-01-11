using Instagram.Models;
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
    /// Interaction logic for ReplyCommentView.xaml
    /// </summary>
    public partial class ReplyCommentView : UserControl
    {
        public ReplyCommentView(CommentResponse commentResponse, int userId)
        {
            InitializeComponent();
            DataContext = new ReplyCommentViewModel(commentResponse, userId);
        }
    }
}
