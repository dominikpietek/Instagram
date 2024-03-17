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
            IAbstractFactory<StoryUserView> storyFactory,
            IAbstractFactory<HomeUserControl> homeFactory,
            IAbstractFactory<ProfileUserControl> profileFactory,
            IAbstractFactory<MessengerUserControl> messengerFactory,
            IAbstractFactory<FriendRequestView> friendRequestFactory,
            IAbstractFactory<MaybeFriendView> maybeFriendFactory,
            IAbstractFactory<CheckProfileUserControl> checkProfileFactory,
            IAbstractFactory<SearchedUserView> searchedUserFactory
            )
        {
            InitializeComponent();
            DataContext = new FeedViewModel(CloseWindow, MainContainer, this.Resources, newPostFactory, loginFactory, homeFactory, storyFactory, profileFactory, messengerFactory, friendRequestFactory, maybeFriendFactory, checkProfileFactory, searchedUserFactory, db, IsMouseOverSearchingFriends);
        }

        public void CloseWindow()
        {
            this.Close();
        }

        public bool IsMouseOverSearchingFriends()
        {
            return (this.searchScroll.IsMouseOver || this.searchingBox.IsMouseOver);
        }
    }
}
