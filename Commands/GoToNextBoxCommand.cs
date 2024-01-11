using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Instagram.Commands
{
    public class GoToNextBoxCommand : CommandBase
    {
        private Action _ChangeBoxIndex;
        public GoToNextBoxCommand(Action ChangeBoxIndex)
        {
            _ChangeBoxIndex = ChangeBoxIndex;
        }
        public override void Execute(object parameter)
        {
            _ChangeBoxIndex.Invoke();
        }
    }
}
