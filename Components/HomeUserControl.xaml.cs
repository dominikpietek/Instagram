using Instagram.Databases;
using Instagram.Models;
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
        public static DependencyProperty HomeSourceProperty = DependencyProperty.Register("HomeSource", typeof(ObservableCollection<PostView>), typeof(HomeUserControl), new PropertyMetadata(new ObservableCollection<PostView>()));
        public ObservableCollection<PostView> HomeSource 
        { 
            get { return (ObservableCollection<PostView>)GetValue(HomeSourceProperty); }
            set { SetValue(HomeSourceProperty, value); } 
        }
        private ObservableCollection<PostView> _postsSection;
        private int _userId;
        public HomeUserControl(int userId)
        {
            DataContext = this;
            InitializeComponent();
            _userId = userId;
            ShowPosts();
            HomeSource = _postsSection;
        }
        private void ShowPosts()
        {
            // start from end
            // show friends's posts too
            // load more posts button
            _postsSection = new ObservableCollection<PostView>();
            using (var _db = new InstagramDbContext())
            {
                foreach (Post post in _db.Posts.Where(p => !p.OnlyForFriends).Take(10))
                {
                    _postsSection.Add(new PostView(post, _userId, ShowPosts));
                }
            }
        }
    }
}
