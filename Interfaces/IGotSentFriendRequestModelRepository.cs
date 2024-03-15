using Instagram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Interfaces
{
    public interface IGotSentFriendRequestModelRepository
    {
        Task<List<int>> GetAllAsync(int userId);
        Task<bool> AddAsync(FriendRequestAbstractModel userIdModel);
        Task<bool> RemoveAsync(int userId, int requestUserId);
    }
}
