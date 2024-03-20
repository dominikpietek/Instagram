using Instagram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Interfaces
{
    public interface IFriendRepository
    {
        Task<List<int>> GetAllUserFriendsIdAsync(int userId);
        Task<int> GetFriendId(int userId, int friendId);
        Task<bool> AddFriendAsync(int userId, int friendId);
        Task<bool> RemoveFriendAsync(int userId, int friendId);
        Task<Message> GetLastMessageAsync(int friendId);
        Task<bool> IsFriend(int userId, int friendId);
        Task<Friend> GetFriendAsync(int userId, int friendId);
    }
}
