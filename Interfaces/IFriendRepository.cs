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
        Task<bool> AddFriendAsync(int userId);
        Task<bool> RemoveFriendAsync(int userId, int friendId);
        Task<bool> SaveChangesAsync();
    }
}
