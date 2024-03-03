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
using System.Windows.Automation.Peers;

namespace Instagram.Commands
{
    class CreateNewPostOpenWindowCommand : CommandBase
    {
        private readonly IAbstractFactory<CreateNewPostWindowView> _newPostFactory;
        private readonly Func<Task> _UpdatePosts;

        public CreateNewPostOpenWindowCommand(IAbstractFactory<CreateNewPostWindowView> newPostFactory, Func<Task> UpdatePosts)
        {
            _newPostFactory = newPostFactory;
            _UpdatePosts = UpdatePosts;
        }
        public override void Execute(object parameter)
        {
            CreateNewPostWindowView window = _newPostFactory.Create();
            window.InitData(_UpdatePosts);
            window.Show();
        }
    }
}
