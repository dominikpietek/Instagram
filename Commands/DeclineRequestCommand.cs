using Accessibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Commands
{
    public class DeclineRequestCommand : CommandBase
    {
        private Action _RemoveFriend;
        public DeclineRequestCommand(Action RemoveFriend)
        {
            _RemoveFriend = RemoveFriend;
        }
        public override void Execute(object parameter)
        {
            _RemoveFriend.Invoke();
        }
    }
}
