using Instagram.Databases;
using Instagram.Models;
using Instagram.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Instagram.Commands
{
    class CreateNewPostOpenWindowCommand : CommandBase
    {
        private User _user;
        private Action _ShowPosts;
        public CreateNewPostOpenWindowCommand(User user, Action ShowPosts)
        {
            _user = user;
            _ShowPosts = ShowPosts;
        }
        public override void Execute(object parameter)
        {
            Window createNewPostWindow = new CreateNewPostWindowView(_user, _ShowPosts);
            createNewPostWindow.Show();
        }
    }
}
