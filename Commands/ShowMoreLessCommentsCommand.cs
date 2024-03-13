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
        private readonly Func<Task> _ShowMoreLessComments;

        public ShowMoreLessCommentsCommand(Func<Task> ShowMoreLessComments)
        {
            _ShowMoreLessComments = ShowMoreLessComments;
        }
        public override void Execute(object parameter)
        {
            _ShowMoreLessComments.Invoke();
        }
    }
}
