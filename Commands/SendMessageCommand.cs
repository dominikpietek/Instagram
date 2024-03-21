using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Commands
{
    public class SendMessageCommand : CommandBase
    {
        private readonly Func<Task> _SendMessage;

        public SendMessageCommand(Func<Task> SendMessage)
        {
            _SendMessage = SendMessage;
        }
        public async override void Execute(object parameter)
        {
            await _SendMessage.Invoke();
        }
    }
}
