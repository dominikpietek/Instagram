using Instagram.Interfaces;
using Instagram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Instagram.Commands
{
    public class UpdateAccountCommand : CommandBase
    {
        private readonly IUserRepository _userRepository;
        private readonly Func<User> _user;

        public UpdateAccountCommand(IUserRepository userRepository, Func<User> user)
        {
            _userRepository = userRepository;
            _user = user;
        }
        public async override void Execute(object parameter)
        {
            await _userRepository.UpdateUserAsync(_user.Invoke());
            MessageBox.Show("User Data Updated!");
        }
    }
}
