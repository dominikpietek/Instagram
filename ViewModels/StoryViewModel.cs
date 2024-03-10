using Instagram.Commands;
using Instagram.Databases;
using Instagram.Interfaces;
using Instagram.Models;
using Instagram.Repositories;
using Instagram.Services;
using Instagram.StartupHelpers;
using Instagram.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Instagram.ViewModels
{
    public class StoryViewModel : ViewModelBase
    {
        #region OnPropertyChangedProperties
        private bool _IsUserAuthor;
        public bool IsUserAuthor
        {
            get { return _IsUserAuthor; }
            set
            {
                _IsUserAuthor = value;
                OnPropertyChanged(nameof(IsUserAuthor));
            }
        }
        private string _PlusIcon;
        public string PlusIcon
        {
            get { return _PlusIcon; }
            set
            {
                _PlusIcon = value;
                OnPropertyChanged(nameof(PlusIcon));
            }
        }
        private string _TrashIcon;
        public string TrashIcon
        {
            get { return _TrashIcon; }
            set
            {
                _TrashIcon = value;
                OnPropertyChanged(nameof(TrashIcon));
            }
        }
        private int _HowManyStories;
        public int HowManyStories
        {
            get { return _HowManyStories; }
            set
            {
                _HowManyStories = value;
                OnPropertyChanged(nameof(HowManyStories));
            }
        }
        private int _StoryIndex = 1;
        public int StoryIndex 
        { 
            get { return _StoryIndex; }
            set
            {
                _StoryIndex = value;
                OnPropertyChanged(nameof(StoryIndex));
            }
        }
        private string _LeftArrowIcon;
        public string LeftArrowIcon
        {
            get { return _LeftArrowIcon; }
            set
            {
            _LeftArrowIcon = value;
                OnPropertyChanged(nameof(LeftArrowIcon));
            }
        }
        private string _RightArrowIcon;
        public string RightArrowIcon
        {
            get { return _RightArrowIcon; }
            set
            {
                _RightArrowIcon = value;
                OnPropertyChanged(nameof(RightArrowIcon));
            }
        }
        private string _Nickname;
        public string Nickname 
        { 
            get { return _Nickname; }
            set
            {
                _Nickname = value;
                OnPropertyChanged(nameof(Nickname));
            }
        }
        private string _PublicationDate;
        public string PublicationDate
        {
            get { return _PublicationDate; }
            set
            {
                _PublicationDate = value;
                OnPropertyChanged(nameof(PublicationDate));
            }
        }
        private BitmapImage _ImageSource;
        public BitmapImage ImageSource
        {
            get { return _ImageSource; }
            set
            {
                _ImageSource = value;
                OnPropertyChanged(nameof(ImageSource));
            }
        }
        #endregion
        #region PrivateProperties
        private readonly List<int> _storyIds;
        private readonly IStoryRepository _storyRepository;
        private readonly string _path;
        private readonly int _authorId;
        #endregion
        #region Commands
        public ICommand LeftArrowButton { get; set; }
        public ICommand RightArrowButton { get; set; }
        public ICommand AddStoryButton { get; set; }
        public ICommand DeleteStoryButton { get; set; }
        #endregion
        public StoryViewModel(InstagramDbContext db, List<int> storyIds, IAbstractFactory<CreateNewStoryView> storyFactory, int authorId)
        {
            #region PrivatePropertiesAssignement
            _storyIds = storyIds;
            _storyRepository = new StoryRepository(db);
            _path = ConfigurationManager.AppSettings["ResourcesPath"]!;
            _authorId = authorId;
            HowManyStories = storyIds.Count();
            #endregion
            #region Commands
            LeftArrowButton = new ChangeStoryCommand(false, ChangeStory);
            RightArrowButton = new ChangeStoryCommand(true, ChangeStory);
            AddStoryButton = new CreateStoryCommand(storyFactory);
            DeleteStoryButton = new DeleteStoryCommand(_storyRepository, StoryIndex - 1); //update stories
            #endregion
            InitAsync();
        }

        private async Task InitAsync()
        {
            await ChangeStory();
            LeftArrowIcon = $"{_path}/leftArrowIcon.png";
            RightArrowIcon = $"{_path}/rightArrowIcon.png";
            IsUserAuthor = await GetUser.IdFromFile() == _authorId ? true : false;
            PlusIcon = $"{_path}/plusIcon.png";
            TrashIcon = $"{_path}/trashIcon.png";
        }

        private async Task ChangeStory(int nextOrPrevious = 0)
        {
            if (nextOrPrevious == 0)
            {
                StoryIndex = StoryIndex;
            }
            else if (StoryIndex + nextOrPrevious < 1)
            {
                StoryIndex = HowManyStories;
            }
            else if (StoryIndex + nextOrPrevious > HowManyStories)
            {
                StoryIndex = 1;
            }
            else
            {
                StoryIndex++;
            }
            Story story = await _storyRepository.GetStoryAsync(_storyIds[StoryIndex - 1]);
            PublicationDate = HowManyHoursAgo(story.PublicationDate);
            ImageSource = ConvertImage.FromByteArray(story.Image.ImageBytes);
            Nickname = story.User.Nickname;
        }

        private string HowManyHoursAgo(DateTime publicationDate)
        {
            return $"published: {(DateTime.Now - publicationDate).Hours} hours ago";
        }
    }
}
