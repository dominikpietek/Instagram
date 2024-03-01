using Instagram.Commands;
using Instagram.Components;
using Instagram.Databases;
using Instagram.Interfaces;
using Instagram.Models;
using Instagram.Repositories;
using Instagram.StartupHelpers;
using Instagram.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        private readonly IPostRepository _postRepository;
        private int _loadedPosts = 1;
        private ObservableCollection<PostView> _posts = new ObservableCollection<PostView>();
        private IAbstractFactory<PostView> _postFactory;
        #endregion
        #region DependencyProperty
        public static DependencyProperty HomeSourceProperty =
            DependencyProperty.Register(
                "HomeSource",
                typeof(ObservableCollection<PostView>),
                typeof(HomeUserControl),
                new PropertyMetadata(new ObservableCollection<PostView>()));
        public ObservableCollection<PostView> HomeSource
        {
            get { return (ObservableCollection<PostView>)GetValue(HomeSourceProperty); }
            set { SetValue(HomeSourceProperty, value); }
        }
        public static DependencyProperty IsThereMorePostsProperty =
            DependencyProperty.Register("IsThereMorePosts", typeof(bool), typeof(HomeUserControl), new PropertyMetadata(false));
        public bool IsThereMorePosts
        {
            get { return (bool)GetValue(IsThereMorePostsProperty); }
            set { SetValue(IsThereMorePostsProperty, value); }
        }
        #endregion
        public HomeViewModel(InstagramDbContext db, IAbstractFactory<PostView> postFactory)
        {
            _postRepository = new PostRepository(db);
            LoadMoreButton = new LoadMoreButton(LoadMorePosts);
            _postFactory = postFactory;
            ShowPosts();
            HomeSource = _posts;
            IsThereMorePosts = _posts.Count() <= _loadedPosts ? false : true;
        }

        public async Task ShowPosts()
        {
            List<Post> posts = await _postRepository.GetAllPostsWithAllDataToShowAsync();
            posts.Reverse();
            foreach (Post post in posts.Take(_loadedPosts))
            {
                PostView postView = _postFactory.Create();
                postView.AddDataContext(post.Id);
                _posts.Add(postView);
            }
        }

        public void LoadMorePosts()
        {
            _loadedPosts += 5;
            ShowPosts();
        }
    }
}
