using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Commands
{
    public class SearchCommand : CommandBase
    {
        private readonly Action _ChangeValue;

        public SearchCommand(Action ChangeValue)
        {
            _ChangeValue = ChangeValue;
        }
        public override void Execute(object parameter)
        {
            _ChangeValue.Invoke();
        }
    }
}
