using Instagram.StartupHelpers;
using Instagram.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Commands
{
    public class ShowStoryCommand : CommandBase
    {
        private readonly List<int> _storyIds;
        private readonly IAbstractFactory<StoryView> _storyFactory;
        private readonly int _authorId;

        public ShowStoryCommand(List<int> storyIds, IAbstractFactory<StoryView> storyFactory, int authorId)
        {
            _storyIds = storyIds;
            _storyFactory = storyFactory;
            _authorId = authorId;
        }
        public override void Execute(object parameter)
        {
            var story = _storyFactory.Create();
            story.SetDataContext(_storyIds, _authorId);
            story.Show();
        }
    }
}
