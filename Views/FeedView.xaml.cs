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
    /// Interaction logic for FeedsView.xaml
    /// </summary>
    public partial class FeedView : Window
    {
        public FeedView(User user, Action<bool> ChangeLoginTheme)
        {
            InitializeComponent();
            DataContext = new FeedViewModel(user, CloseWindow, MainContainer, ChangeLoginTheme, ChangeFeedTheme);
        }
        private void CloseWindow()
        {
            this.Close();
        }
        private void ChangeFeedTheme(bool isDarkMode)
        {
            this.Resources.MergedDictionaries.Clear();
            string resourceName = isDarkMode ? "DarkModeDictionary" : "BrightModeDictionary";
            ResourceDictionary resourceDictionary = new ResourceDictionary() { Source = new Uri(string.Format("ResourceDictionaries/{0}.xaml", resourceName), UriKind.Relative) };
            this.Resources.MergedDictionaries.Add(resourceDictionary);
        }
    }
}
