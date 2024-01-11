using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Commands
{
    public class ChangeMainContainerContentCommand : CommandBase
    {
        private Action _ChangeContent;
        public ChangeMainContainerContentCommand(Action ChangeContent)
        {
            _ChangeContent = ChangeContent;
        }
        public override void Execute(object parameter)
        {
            _ChangeContent.Invoke();
        }
    }
}
