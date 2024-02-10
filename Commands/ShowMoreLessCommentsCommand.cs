using Instagram.Databases;
using Instagram.JSONModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Commands
{
    public class ShowMoreLessCommentsCommand : CommandBase
    {
        private Func<Task> _ShowMoreLessComments;
        private Action _ChangeTheme;

        public ShowMoreLessCommentsCommand(Func<Task> ShowMoreLessComments, Action ChangeTheme)
        {
            _ShowMoreLessComments = ShowMoreLessComments;
            _ChangeTheme = ChangeTheme;
        }
        public override void Execute(object parameter)
        {
            _ShowMoreLessComments.Invoke();
            _ChangeTheme.Invoke();
        }
    }
}
