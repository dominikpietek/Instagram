using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Instagram.Commands
{
    public class RemoveFriendCommand : CommandBase
    {
        private readonly Func<Task> _RemoveFriend;

        public RemoveFriendCommand(Func<Task> RemoveFriend)
        {
            _RemoveFriend = RemoveFriend;
        }
        public override async void Execute(object parameter)
        {
            await _RemoveFriend.Invoke();
        }
    }
}
