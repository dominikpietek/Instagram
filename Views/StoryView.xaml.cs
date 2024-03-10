using Instagram.Databases;
using Instagram.Services;
using Instagram.StartupHelpers;
using Instagram.ViewModels;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

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
        public void SetDataContext(List<int> storyIds, int authorId)
        {
            this.DataContext = new StoryViewModel(_db, storyIds, _storyFactory, authorId);
        }
    }
}
