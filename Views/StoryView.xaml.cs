using Instagram.Databases;
using Instagram.Services;
using Instagram.StartupHelpers;
using Instagram.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Instagram.Views
{
    public partial class StoryView : Window
    {
        private readonly InstagramDbContext _db;
        private readonly IAbstractFactory<CreateNewStoryView> _storyFactory;

        public StoryView(InstagramDbContext db, IAbstractFactory<CreateNewStoryView> storyFactory)
        {
            InitializeComponent();
            ChangeTheme.ChangeAsync(this.Resources);
            _db = db;
            _storyFactory = storyFactory;
        }

        public void CloseWindow()
        {
            this.Close();
        }
        public void SetDataContext(List<int> storyIds, int authorId, Action ChangePlus)
        {
            this.DataContext = new StoryViewModel(_db, storyIds, _storyFactory, authorId, CloseWindow, ChangePlus);
        }
    }
}
