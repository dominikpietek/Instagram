using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Commands
{
    public class ReplyToCommentCommand : CommandBase
    {
        private Action _ChangeReplyClickedStatus;
        public ReplyToCommentCommand(Action ChangeReplyClickedStatus)
        {
            _ChangeReplyClickedStatus = ChangeReplyClickedStatus;
        }
        public override void Execute(object parameter)
        {
            _ChangeReplyClickedStatus.Invoke();
        }
    }
}
