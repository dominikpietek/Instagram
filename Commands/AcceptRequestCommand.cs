using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Commands
{
    public class AcceptRequestCommand : CommandBase
    {
        private Action _AddFriend;
        public AcceptRequestCommand(Action AddFriend)
        {
            _AddFriend = AddFriend;
        }
        public override void Execute(object parameter)
        {
            _AddFriend.Invoke();
        }
    }
}
