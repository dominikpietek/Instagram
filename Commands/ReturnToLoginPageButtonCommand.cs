using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Commands
{
    public class ReturnToLoginPageButtonCommand : CommandBase
    {
        private Action _CloseWindowAndOpenLoginWindow;
        public ReturnToLoginPageButtonCommand(Action closeWindowAndOpenLoginWindow)
        {
            _CloseWindowAndOpenLoginWindow = closeWindowAndOpenLoginWindow;
        }
        public override void Execute(object parameter)
        {
            _CloseWindowAndOpenLoginWindow.Invoke();
        }
    }
}
