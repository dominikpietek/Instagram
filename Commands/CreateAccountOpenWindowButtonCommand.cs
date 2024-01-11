using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Instagram.Views;

namespace Instagram.Commands
{
    public class CreateAccountOpenWindowButtonCommand : CommandBase
    {
        private Action _CloseWindow;
        public CreateAccountOpenWindowButtonCommand(Action CloseWindow)
        {
            _CloseWindow = CloseWindow;
        }
        public override void Execute(object parameter)
        {
            Window createAccountWindow = new CreateAccountWindowView();
            createAccountWindow.Show();
            _CloseWindow.Invoke();
        }
    }
}
