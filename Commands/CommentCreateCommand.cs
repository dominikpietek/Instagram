using Instagram.Databases;
using Instagram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Commands
{
    public class CommentCreateCommand : CommandBase
    {
        private Func<Task> _CreateNewComment;
        public CommentCreateCommand(Func<Task> CreateNewComment)
        {
            _CreateNewComment = CreateNewComment;
        }
        public override void Execute(object parameter)
        {
            _CreateNewComment.Invoke();
        }
    }
}
