using Instagram.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Instagram.Commands
{
    public class CreateAccountCommand : CommandBase
    {
        private Func<bool> _AddingUserToDatabase;
        private Action _CloseWindowAndOpenLoginWindow;
        public CreateAccountCommand(Func<bool> AddingUserToDatabase, Action CloseWindowAndOpenLoginWindow)
        {
            _AddingUserToDatabase = AddingUserToDatabase;
            _CloseWindowAndOpenLoginWindow = CloseWindowAndOpenLoginWindow;
        }
        public override void Execute(object parameter)
        {
            if (_AddingUserToDatabase.Invoke())
            {
                _CloseWindowAndOpenLoginWindow.Invoke();
            }
        }
    }
}
