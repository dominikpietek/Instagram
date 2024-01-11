using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Commands
{
    public class CreateCommentReplyCommand : CommandBase
    {
        private Action _CreateNewReply;
        public CreateCommentReplyCommand(Action CreateNewReply)
        {
            _CreateNewReply = CreateNewReply;
        }
        public override void Execute(object parameter)
        {
            _CreateNewReply.Invoke();
        }
    }
}
