using Instagram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Interfaces
{
    public interface IMessageRepository
    {
        Task<List<Message>> GetUserMessagesToFriend(int userId, int friendId);
        Task<bool> RemoveMessage(Message message);
        Task<bool> AddMessage(Message message);
        Task<bool> UpdateMessage(Message message);
    }
}
