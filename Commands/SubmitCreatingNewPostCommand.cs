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
        private Func<bool> _CreatPost;
        public SubmitCreatingNewPostCommand(Action CloseWindow, Func<bool> CreatePost)
        {
            _CloseWindow = CloseWindow;
            _CreatPost = CreatePost;
        }
        public override void Execute(object parameter)
        {
            if (_CreatPost.Invoke())
            {
                _CloseWindow.Invoke();
            }
        }
    }
}
