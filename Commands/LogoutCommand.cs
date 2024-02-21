using Instagram.Repositories;
using Instagram.StartupHelpers;
using Instagram.Views;
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
        private readonly Action _CloseWindow;
        private readonly IAbstractFactory<LoginOrRegisterWindowView> _loginFactory;

        public LogoutCommand(Action CloseWindow, IAbstractFactory<LoginOrRegisterWindowView> loginFactory)
        {

            _CloseWindow = CloseWindow;
            _loginFactory = loginFactory;
        }
        public override void Execute(object parameter)
        {
            var logoutRepository = new LogoutRepository();
            logoutRepository.RestartAutomaticLoginAsync();
            logoutRepository.CloseWindowAndShowStartUpWindow(_CloseWindow, _loginFactory);
        }
    }
}
