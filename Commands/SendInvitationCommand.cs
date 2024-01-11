using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Commands
{
    public class SendInvitationCommand : CommandBase
    {
        private Action _ChangeInvitationStatus;
        public SendInvitationCommand(Action ChangeInvitationStatus)
        {
            _ChangeInvitationStatus = ChangeInvitationStatus;
        }
        public override void Execute(object parameter)
        {
            _ChangeInvitationStatus.Invoke();
        }
    }
}
