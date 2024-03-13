using Instagram.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Commands
{
    public class DeleteStoryCommand : CommandBase
    {
        private readonly Func<bool, Task> _UpdatePosts;

        public DeleteStoryCommand(Func<bool, Task> UpdatePosts)
        {
            _UpdatePosts = UpdatePosts;
        }

        public async override void Execute(object parameter)
        {
            await _UpdatePosts.Invoke(true);
        }
    }
}
