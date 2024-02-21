using Instagram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Interfaces
{
    public interface IUserIdGotSentModelRepository
    {
        Task<List<int>> GetAllAsync(int userId);
        Task<bool> AddAsync(UserIdAbstractModel userIdModel);
        Task<bool> RemoveAsync(int id);
        Task<bool> SaveChangesAsync();
    }
}
