using Instagram.Databases;
using Instagram.Interfaces;
using Instagram.Models;
using Instagram.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.ViewModels
{
    public class MessageViewModel
    {
        public string Content { get; set; }
        public bool MyOrHis { get; set; }

        private readonly Message _message;

        public MessageViewModel(Message message)
        {
            _message = message;
            Init();
        }

        private async Task Init()
        {
            int userId = await GetUser.IdFromFile();
            Content = _message.Content;
            if (userId == _message.UserId) MyOrHis = true;
            else MyOrHis = false;
        }
    }
}
