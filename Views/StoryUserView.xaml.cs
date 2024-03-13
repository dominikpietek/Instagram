using Instagram.Databases;
using Instagram.Services;
using Instagram.StartupHelpers;
using Instagram.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace Instagram.Views
{
    public partial class StoryUserView : UserControl
    {
        private readonly InstagramDbContext _db;
        private readonly IAbstractFactory<StoryView> _storyFactory;
        private readonly IAbstractFactory<CreateNewStoryView> _createStoryFactory;
        public StoryUserView(InstagramDbContext db, IAbstractFactory<StoryView> storyFactory, IAbstractFactory<CreateNewStoryView> createStoryFactory)
        {
            _db = db;
            _storyFactory = storyFactory;
            _createStoryFactory = createStoryFactory;
            InitializeComponent();
        }

        public void SetDataContext(List<int> storyIds, int userId)
        {
            DataContext = new StoryUserViewModel(_db, storyIds, _storyFactory, _createStoryFactory, userId);
        }
    }
}
