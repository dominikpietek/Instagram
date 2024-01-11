using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Commands
{
    public class CommentButtonCommand : CommandBase
    {
        private Action _UpdateIsCommentClickedToCreateValue;
        public CommentButtonCommand(Action UpdateIsCommentClickedToCreateValue)
        {
            _UpdateIsCommentClickedToCreateValue = UpdateIsCommentClickedToCreateValue;
        }
        public override void Execute(object parameter)
        {
            _UpdateIsCommentClickedToCreateValue.Invoke();
        }
    }
}
