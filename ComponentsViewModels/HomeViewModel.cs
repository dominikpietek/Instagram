using Instagram.Commands;
using Instagram.Components;
using Instagram.Databases;
using Instagram.Interfaces;
using Instagram.Models;
using Instagram.Repositories;
using Instagram.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Instagram.ComponentsViewModels
{
    public class HomeViewModel : DependencyObject
    {
        #region Commands
        public ICommand LoadMoreButton { get; set; }
        #endregion
        #region PrivateProperties
        private IPostRepository _postRepository;
        private int _loadedPosts = 10;
        #endregion
        #region DependencyProperty
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
        #endregion
        public HomeViewModel(InstagramDbContext db)
        {
            _postRepository = new PostRepository(db);
            LoadMoreButton = new LoadMoreButton(LoadMorePosts);
            ShowPosts();
        }

        public async Task ShowPosts()
        {
            List<Post> posts = await _postRepository.GetAllPostsWithAllDataToShowAsync();
            IsThereMorePosts = posts.Count <= _loadedPosts ? false : true;
            posts.Take(_loadedPosts);
            posts.Reverse();
            foreach (Post post in posts)
            {
                HomeSource.Add(new PostView(post, ShowPosts));
            }
        }

        public void LoadMorePosts()
        {
            _loadedPosts += 10;
            ShowPosts();
        }
    }
}
