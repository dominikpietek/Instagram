using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Commands
{
    public class SendInvitationCommand : CommandBase
    {
        private readonly Func<Task> _ChangeInvitationStatus;
        public SendInvitationCommand(Func<Task> ChangeInvitationStatus)
        {
            _ChangeInvitationStatus = ChangeInvitationStatus;
        }
        public async override void Execute(object parameter)
        {
            await _ChangeInvitationStatus.Invoke();
        }
    }
}
