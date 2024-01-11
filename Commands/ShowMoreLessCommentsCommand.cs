using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Commands
{
    public class ShowMoreLessCommentsCommand : CommandBase
    {
        private Action _ShowMoreLessComments;
        public ShowMoreLessCommentsCommand(Action ShowMoreLessComments)
        {
            _ShowMoreLessComments = ShowMoreLessComments;
        }
        public override void Execute(object parameter)
        {
            _ShowMoreLessComments.Invoke();
        }
    }
}
