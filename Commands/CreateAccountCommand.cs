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
        private Func<Task<bool>> _AddingUserToDatabase;
        private Action _CloseWindowAndOpenLoginWindow;
        public CreateAccountCommand(Func<Task<bool>> AddingUserToDatabase, Action CloseWindowAndOpenLoginWindow)
        {
            _AddingUserToDatabase = AddingUserToDatabase;
            _CloseWindowAndOpenLoginWindow = CloseWindowAndOpenLoginWindow;
        }
        public async override void Execute(object parameter)
        {
            if (await _AddingUserToDatabase.Invoke())
            {
                _CloseWindowAndOpenLoginWindow.Invoke();
            }
        }
    }
}
