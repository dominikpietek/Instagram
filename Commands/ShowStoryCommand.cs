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
        private readonly Action _ChangePlus;

        public ShowStoryCommand(List<int> storyIds, IAbstractFactory<StoryView> storyFactory, int authorId, Action ChangePlus)
        {
            _storyIds = storyIds;
            _storyFactory = storyFactory;
            _authorId = authorId;
            _ChangePlus = ChangePlus;
        }
        public override void Execute(object parameter)
        {
            var story = _storyFactory.Create();
            story.SetDataContext(_storyIds, _authorId, _ChangePlus);
            story.Show();
        }
    }
}
