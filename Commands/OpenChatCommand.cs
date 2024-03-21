using Instagram.Components;
using Instagram.StartupHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Instagram.Commands
{
    public class OpenChatCommand : CommandBase
    {
        private readonly Func<int, Task> _ShowMessages;
        private readonly int _friendId;

        public OpenChatCommand(Func<int, Task> ShowMessages, int friendId)
        {
            _ShowMessages = ShowMessages;
            _friendId = friendId;
        }
        public override void Execute(object parameter)
        {
            _ShowMessages.Invoke(_friendId);
        }
    }
}
