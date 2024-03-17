using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Commands
{
    public class ChangeFriendShipStatusCommand : CommandBase
    {
        private readonly Func<Task> _FriendshipStatusAsync;

        public ChangeFriendShipStatusCommand(Func<Task> FriendshipStatusAsync)
        {
            _FriendshipStatusAsync = FriendshipStatusAsync;
        }
        public async override void Execute(object parameter)
        {
            await _FriendshipStatusAsync.Invoke();
        }
    }
}
