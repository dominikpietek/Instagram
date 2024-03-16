using Instagram.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Commands
{
    public class ShowProfileCommand : CommandBase
    {
        private readonly Action<int> _ShowCheckProfile;
        private readonly int _profileId;

        public ShowProfileCommand(Action<int> ShowCheckProfile, int profileId)
        {
            _ShowCheckProfile = ShowCheckProfile;
            _profileId = profileId;
        }
        public override void Execute(object parameter)
        {
            _ShowCheckProfile.Invoke(_profileId);
        }
    }
}
