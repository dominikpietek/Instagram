using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Commands
{
    public class SubmitCreatingNewPostCommand : CommandBase
    {
        private Action _CloseWindow;
        private Action _CreatPost;
        public SubmitCreatingNewPostCommand(Action CloseWindow, Action CreatePost)
        {
            _CloseWindow = CloseWindow;
            _CreatPost = CreatePost;
        }
        public override void Execute(object parameter)
        {
            _CreatPost.Invoke();
            _CloseWindow.Invoke();
        }
    }
}
