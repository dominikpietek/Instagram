using Instagram.Databases;
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
using System.Windows.Shapes;

namespace Instagram.Views
{
    /// <summary>
    /// Interaction logic for CreateNewPostWindowView.xaml
    /// </summary>
    public partial class CreateNewPostWindowView : Window
    {
        private Action _ShowPosts;
        public CreateNewPostWindowView(User user, Action ShowPosts)
        {
            _ShowPosts = ShowPosts;
            InitializeComponent();
            DataContext = new CreatePostViewModel(CloseWindow, user, ChangeCreatePostTheme);
        }
        public void CloseWindow()
        {
            //using (var db = new InstagramDbContext("MainDb"))
            //{
            //    _ShowPosts.Invoke();
            //}
            this.Close();
        }
        public void ChangeCreatePostTheme(bool isDarkMode)
        {
            this.Resources.MergedDictionaries.Clear();
            string resourceName = isDarkMode ? "DarkModeDictionary" : "BrightModeDictionary";
            ResourceDictionary resourceDictionary = new ResourceDictionary() { Source = new Uri(string.Format("ResourceDictionaries/{0}.xaml", resourceName), UriKind.Relative) };
            this.Resources.MergedDictionaries.Add(resourceDictionary);
        }
    }
}
