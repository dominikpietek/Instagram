using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Commands
{
    public class ChangeSearchingCommand : CommandBase
    {
        private readonly Action _GenerateSearchingUsers;

        public ChangeSearchingCommand(Action GenerateSearchingUsers)
        {
            _GenerateSearchingUsers = GenerateSearchingUsers;
        }
        public override void Execute(object parameter)
        {
            _GenerateSearchingUsers.Invoke();
        }
    }
}
