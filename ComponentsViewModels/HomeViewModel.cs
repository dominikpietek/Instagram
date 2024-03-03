using Instagram.Commands;
using Instagram.Components;
using Instagram.Databases;
using Instagram.Interfaces;
using Instagram.Models;
using Instagram.Repositories;
using Instagram.StartupHelpers;
using Instagram.ViewModels;
using Instagram.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Instagram.ComponentsViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        #region Commands
        public ICommand LoadMoreButton { get; set; }
        #endregion
        #region PrivateProperties
        private readonly IPostRepository _postRepository;
        private int _loadedPosts = 5;
        private readonly IAbstractFactory<PostView> _postFactory;
        private readonly Action _ChangeHomeTheme;
        #endregion
        #region OnPropertyChangedProperties
        private ObservableCollection<PostView> _HomeSource;
        public ObservableCollection<PostView> HomeSource 
        { 
            get { return _HomeSource; }
            set
            {
                _HomeSource = value;
                OnPropertyChanged(nameof(HomeSource));
            }
        }
        private bool _IsThereMorePosts;
        public bool IsThereMorePosts
        {
            get { return _IsThereMorePosts; }
            set 
            {
                _IsThereMorePosts = value;
                OnPropertyChanged(nameof(IsThereMorePosts));
            }
        }
        #endregion
        public HomeViewModel(InstagramDbContext db, IAbstractFactory<PostView> postFactory, Action ChangeHomeTheme)
        {
            _postRepository = new PostRepository(db);
            LoadMoreButton = new LoadMoreCommand(LoadMorePosts);
            _postFactory = postFactory;
            _ChangeHomeTheme = ChangeHomeTheme;
            ShowPosts();
        }

        public async Task ShowPosts()
        {
            List<Post> posts = await _postRepository.GetAllPostsWithAllDataToShowAsync();
            HomeSource = new ObservableCollection<PostView>();
            posts.Reverse();
            foreach (Post post in posts.Take(_loadedPosts))
            {
                PostView postView = _postFactory.Create();
                postView.AddDataContext(post.Id, _ChangeHomeTheme);
                HomeSource.Add(postView);
            }
            IsThereMorePosts = posts.Count() <= _loadedPosts ? false : true;
        }

        public void LoadMorePosts()
        {
            _loadedPosts += 5;
            ShowPosts();
        }
    }
}
