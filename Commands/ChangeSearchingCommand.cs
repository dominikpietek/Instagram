using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Commands
{
    public class ChangeSearchingCommand : CommandBase
    {
        private readonly Func<Task> _GenerateSearchingUsers;

        public ChangeSearchingCommand(Func<Task> GenerateSearchingUsers)
        {
            _GenerateSearchingUsers = GenerateSearchingUsers;
        }
        public override void Execute(object parameter)
        {
            _GenerateSearchingUsers.Invoke();
        }
    }
}
