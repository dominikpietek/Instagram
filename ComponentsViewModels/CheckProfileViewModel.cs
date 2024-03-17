using Instagram.Commands;
using Instagram.Databases;
using Instagram.Interfaces;
using Instagram.Models;
using Instagram.Repositories;
using Instagram.Services;
using Instagram.StartupHelpers;
using Instagram.ViewModels;
using Instagram.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Instagram.ComponentsViewModels
{
    public class CheckProfileViewModel : ViewModelBase
    {
        #region OnPropertyChangedProperties
        private string _ButtonContent;
        public string ButtonContent
        {
            get { return _ButtonContent; }
            set
            {
                _ButtonContent = value;
                OnPropertyChanged(nameof(ButtonContent));
            }
        }
        private BitmapImage _ProfilePhotoSource;
        public BitmapImage ProfilePhotoSource 
        {
            get { return _ProfilePhotoSource; }
            set
            {
                _ProfilePhotoSource = value;
                OnPropertyChanged(nameof(ProfilePhotoSource));
            }
        }
        private int _PostsNumber;
        public int PostsNumber 
        {
            get { return _PostsNumber; }
            set
            {
                _PostsNumber = value;
                OnPropertyChanged(nameof(PostsNumber));
            }
        }
        private int _FriendsNumber;
        public int FriendsNumber 
        {
            get { return _FriendsNumber; }
            set
            {
                _FriendsNumber = value;
                OnPropertyChanged(nameof(FriendsNumber));
            }
        }
        private int _SinceYear;
        public int SinceYear 
        {
            get { return _SinceYear; }
            set
            {
                _SinceYear = value;
                OnPropertyChanged(nameof(SinceYear));
            }
        }
        private ObservableCollection<PostView> _PostSection = new ObservableCollection<PostView>();
        public ObservableCollection<PostView> PostSection 
        { 
            get { return _PostSection; }
            set
            {
                _PostSection = value;
                OnPropertyChanged(nameof(PostSection));
            }
        }
        #endregion
        #region PrivateProperties
        private readonly int _profileId;
        private readonly IGotSentFriendRequestModelRepository _gotRequestRepository;
        private readonly IGotSentFriendRequestModelRepository _sentRequestRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;
        private readonly IAbstractFactory<PostView> _postFactory;
        private readonly Func<Task> _ChangeProfileTheme;
        private readonly IFriendRepository _friendRepository;
        #endregion
        public ICommand ChangeFriendShipStatus { get; set; }
        public CheckProfileViewModel(InstagramDbContext db, IAbstractFactory<PostView> postFactory, int profileId, Func<Task> ChangeProfileTheme)
        {
            #region PrivateProperties
            _profileId = profileId;
            _userRepository = new UserRepository(db);
            _postRepository = new PostRepository(db);
            _friendRepository = new FriendRepository(db);
            _gotRequestRepository = new GotSentFriendRequestModelRepository<GotFriendRequestModel>(db);
            _sentRequestRepository = new GotSentFriendRequestModelRepository<SentFriendRequestModel>(db);
            _postFactory = postFactory;
            _ChangeProfileTheme = ChangeProfileTheme;
            #endregion
            ChangeFriendShipStatus = new ChangeFriendShipStatusCommand(ChangeFriendShipStatusAsync);
            InitAsync();
        }

        private async Task InitAsync()
        {
            User user = await _userRepository.GetUserWithPhotoAndRequestsAsync(_profileId);
            List<Post> posts = await _postRepository.GetUserPostsWithAllDataToShowAsync(_profileId);
            List<int> friends = await _friendRepository.GetAllUserFriendsIdAsync(_profileId);
            ProfilePhotoSource = ConvertImage.FromByteArray(user.ProfilePhoto.ImageBytes);
            PostsNumber = posts.Count;
            FriendsNumber = friends.Count;
            SinceYear = user.Birthdate.Year;
            foreach (Post post in posts)
            {
                PostView postView = _postFactory.Create();
                postView.AddDataContext(post.Id, _ChangeProfileTheme, InitAsync);
                PostSection.Add(postView);
            }
            await FriendshipStatusAsync();
        }

        public async Task ChangeFriendShipStatusAsync()
        {
            int userId = await GetUser.IdFromFile();
            if (ButtonContent == "REMOVE FRIEND")
            {
                await _friendRepository.RemoveFriendAsync(userId, _profileId);
            }
            else if (ButtonContent == "CANCEL REQUEST")
            {
                await _sentRequestRepository.RemoveAsync(userId, _profileId);
                await _gotRequestRepository.RemoveAsync(_profileId, userId);
            }
            else if (ButtonContent == "ACCEPT REQUEST")
            {
                await _friendRepository.AddFriendAsync(userId, _profileId);
                await _gotRequestRepository.RemoveAsync(userId, _profileId);
                await _sentRequestRepository.RemoveAsync(_profileId, userId);
            }
            else
            {
                await _sentRequestRepository.AddAsync(new SentFriendRequestModel() { UserId = userId, StoredUserId = _profileId });
                await _gotRequestRepository.AddAsync(new GotFriendRequestModel() { UserId = _profileId, StoredUserId = userId });
            }
            await FriendshipStatusAsync();
        }

        public async Task FriendshipStatusAsync()
        {
            int userId = await GetUser.IdFromFile();
            if (await _friendRepository.IsFriend(userId, _profileId))
            {
                ButtonContent = "REMOVE FRIEND";
            }
            else if (await _sentRequestRepository.IsRequest(userId, _profileId))
            {
                ButtonContent = "CANCEL REQUEST";
            }
            else if (await _gotRequestRepository.IsRequest(_profileId, userId))
            {
                ButtonContent = "ACCEPT REQUEST";
            }
            else
            {
                ButtonContent = "ADD FRIEND";
            }
        }
    }
}
