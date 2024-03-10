using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Commands
{
    public class ChangeStoryCommand : CommandBase
    {
        private readonly bool _nextStory;
        private readonly Func<int, Task> _ChangeStory;

        public ChangeStoryCommand(bool nextStory, Func<int, Task> ChangeStory)
        {
            _nextStory = nextStory;
            _ChangeStory = ChangeStory;
        }
        public override void Execute(object parameter)
        {
            if (_nextStory)
            {
                _ChangeStory.Invoke(1);
            }
            else
            {
                _ChangeStory.Invoke(-1);
            }
        }
    }
}
