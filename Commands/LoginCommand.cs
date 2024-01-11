using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Commands
{
    public class LoginCommand : CommandBase
    {
        private Action _Login;
        public LoginCommand(Action Login)
        {
            _Login = Login;
        }
        public override void Execute(object parameter)
        {
            _Login.Invoke();
        }
    }
}
