using Instagram.StartupHelpers;
using Instagram.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Commands
{
    public class CreateStoryCommand : CommandBase
    {
        private readonly IAbstractFactory<CreateNewStoryView> _createStoryFactory;
        private readonly Func<bool, Task> _UpdateStories;

        public CreateStoryCommand(IAbstractFactory<CreateNewStoryView> createStoryFactory, Func<bool, Task> UpdateStories)
        {
            _createStoryFactory = createStoryFactory;
            _UpdateStories = UpdateStories;
        }

        public override void Execute(object parameter)
        {
            var factory = _createStoryFactory.Create();
            factory.SetDataContext(_UpdateStories);
            factory.Show();
        }
    }
}
