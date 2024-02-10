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
    /// Interaction logic for CommentView.xaml
    /// </summary>
    public partial class CommentView : UserControl
    {
        private CommentViewModel _ViewModel;
        public CommentView(Comment comment, int userId)
        {
            InitializeComponent();
            _ViewModel = new CommentViewModel(comment, userId);
            DataContext = _ViewModel;
        }
        public void ChangeCommentTheme(bool isDarkMode)
        {
            this.Resources.MergedDictionaries.Clear();
            string resourceName = isDarkMode ? "DarkModeDictionary" : "BrightModeDictionary";
            ResourceDictionary resourceDictionary = new ResourceDictionary() { Source = new Uri(string.Format("ResourceDictionaries/{0}.xaml", resourceName), UriKind.Relative) };
            this.Resources.MergedDictionaries.Add(resourceDictionary);
            _ViewModel.ChangeTheme();
        }
    }
}
