using Instagram.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Instagram.Commands
{
    public class LogoutCommand : CommandBase
    {
        private Action _CloseWindow;
        public LogoutCommand(Action CloseWindow)
        {

            _CloseWindow = CloseWindow;
        }
        public override void Execute(object parameter)
        {
            Logout.RestartAutomaticLogin();
            Logout.CloseWindowAndShowStartUpWindow(_CloseWindow);
        }
    }
}
