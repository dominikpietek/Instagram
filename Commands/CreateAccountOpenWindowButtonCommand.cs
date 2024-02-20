using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Instagram.StartupHelpers;
using Instagram.Views;

namespace Instagram.Commands
{
    public class CreateAccountOpenWindowButtonCommand : CommandBase
    {
        private Action _CloseWindow;
        private IAbstractFactory<CreateAccountWindowView> _factory;
        public CreateAccountOpenWindowButtonCommand(Action CloseWindow, IAbstractFactory<CreateAccountWindowView> factory)
        {
            _CloseWindow = CloseWindow;
            _factory = factory;
        }

        public override void Execute(object parameter)
        {
            _factory.Create().Show();
            _CloseWindow.Invoke();
        }
    }
}
