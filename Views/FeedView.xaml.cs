using Instagram.Components;
using Instagram.Databases;
using Instagram.StartupHelpers;
using Instagram.ViewModels;
using System;
using System.Windows;

namespace Instagram.Views
{
    public partial class FeedView : Window
    {
        public FeedView(
            InstagramDbContext db, 
            IAbstractFactory<CreateNewPostWindowView> newPostFactory,
            IAbstractFactory<LoginOrRegisterWindowView> loginFactory,
            IAbstractFactory<HomeUserControl> homeFactory)
        {
            InitializeComponent();
            DataContext = new FeedViewModel(CloseWindow, MainContainer, this.Resources, newPostFactory, loginFactory, homeFactory, db);
        }

        public void CloseWindow()
        {
            this.Close();
        }
    }
}
