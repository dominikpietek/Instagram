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

        public CreateStoryCommand(IAbstractFactory<CreateNewStoryView> createStoryFactory)
        {
            _createStoryFactory = createStoryFactory;
        }

        public override void Execute(object parameter)
        {
            _createStoryFactory.Create().Show();
        }
    }
}
