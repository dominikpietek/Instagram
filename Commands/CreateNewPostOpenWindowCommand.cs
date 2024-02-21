using Instagram.Databases;
using Instagram.Models;
using Instagram.StartupHelpers;
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
        private readonly IAbstractFactory<CreateNewPostWindowView> _newPostFactory;

        public CreateNewPostOpenWindowCommand(IAbstractFactory<CreateNewPostWindowView> newPostFactory)
        {
            _newPostFactory = newPostFactory;
        }
        public override void Execute(object parameter)
        {
            _newPostFactory.Create().Show();
        }
    }
}
