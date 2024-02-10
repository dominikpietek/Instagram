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
        private PostViewModel _ViewModel;
        public PostView(Post post, int actualUserId, Func<Task> ShowPosts)
        {
            InitializeComponent();
            _ViewModel = new PostViewModel(post, actualUserId, ShowPosts);
            DataContext = _ViewModel;
        }
        public void ChangePostTheme(bool isDarkMode)
        {
            this.Resources.MergedDictionaries.Clear();
            string resourceName = isDarkMode ? "DarkModeDictionary" : "BrightModeDictionary";
            ResourceDictionary resourceDictionary = new ResourceDictionary() { Source = new Uri(string.Format("ResourceDictionaries/{0}.xaml", resourceName), UriKind.Relative) };
            this.Resources.MergedDictionaries.Add(resourceDictionary);
            _ViewModel.ChangeTheme();
        }
    }
}
