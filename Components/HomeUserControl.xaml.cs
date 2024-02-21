using Instagram.Databases;
using Instagram.Interfaces;
using Instagram.JSONModels;
using Instagram.Models;
using Instagram.Repositories;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Instagram.Components
{
    /// <summary>
    /// Interaction logic for HomeUserControl.xaml
    /// </summary>
    public partial class HomeUserControl : UserControl
    {
        public static DependencyProperty HomeSourceProperty = 
            DependencyProperty.Register("HomeSource", typeof(ObservableCollection<PostView>), typeof(HomeUserControl), new PropertyMetadata(new ObservableCollection<PostView>()));
        public ObservableCollection<PostView> HomeSource 
        { 
            get { return (ObservableCollection<PostView>)GetValue(HomeSourceProperty); }
            set { SetValue(HomeSourceProperty, value); } 
        }
        public static DependencyProperty IsThereMorePostsProperty =
            DependencyProperty.Register("IsThereMorePosts", typeof(bool), typeof(HomeUserControl), new PropertyMetadata(true));
        public bool IsThereMorePosts
        {
            get { return (bool)GetValue(IsThereMorePostsProperty); }
            set { SetValue(IsThereMorePostsProperty, value); }
        }
        private ObservableCollection<PostView> _postsSection;
        private int _userId;
        private IPostRepository _postRepository;
        private int _loadedPosts;
        private InstagramDbContext _db;
        public HomeUserControl(int userId, InstagramDbContext db)
        {
            _db = db;
            ChangeHomeThemeAsync();
            _loadedPosts = 10;
            DataContext = this;
            InitializeComponent();
            _userId = userId;
            ShowPosts();
            HomeSource = _postsSection;
        }
        private async Task ChangeHomeThemeAsync()
        {
            JSON<UserDataModel> userJSON = new JSON<UserDataModel>("UserData");
            ChangeHomeTheme(await userJSON.GetDarkModeAsync());
        }
        public async Task ShowPosts()
        {
            _postsSection = new ObservableCollection<PostView>();
            var posts = await _postRepository.GetAllPostsWithAllDataToShowAsync();
            IsThereMorePosts = posts.Count <= _loadedPosts ? false : true;
            posts.Reverse();
            foreach (var post in posts)
            {
                _postsSection.Add(new PostView(post, _userId, ShowPosts, _db));
            }
            _postsSection.Take(_loadedPosts);
        }
        public void ChangeHomeTheme(bool isDarkMode)
        {
            this.Resources.MergedDictionaries.Clear();
            string resourceName = isDarkMode ? "DarkModeHomeDictionary" : "BrightModeDictionary";
            ResourceDictionary resourceDictionary = new ResourceDictionary() { Source = new Uri(string.Format("ResourceDictionaries/{0}.xaml", resourceName), UriKind.Relative) };
            this.Resources.MergedDictionaries.Add(resourceDictionary);
            foreach (var post in HomeSource)
            {
                post.ChangePostTheme(isDarkMode);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _loadedPosts += 10;
            ShowPosts();
        }
    }
}
