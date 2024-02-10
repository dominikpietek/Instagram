using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Commands
{
    public class CreateCommentReplyCommand : CommandBase
    {
        private Func<Task> _CreateNewReply;
        public CreateCommentReplyCommand(Func<Task> CreateNewReply)
        {
            _CreateNewReply = CreateNewReply;
        }
        public override void Execute(object parameter)
        {
            _CreateNewReply.Invoke();
        }
    }
}
