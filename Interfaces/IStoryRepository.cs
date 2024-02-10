using Instagram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Interfaces
{
    public interface IStoryRepository
    {
        Task<List<Story>> GetAllStoriesAsync();
        Task<Story> GetStoryAsync(int id);
        Task<bool> RemoveStoryAsync(int id);
        Task<bool> UpdateStoryAsync(Story story);
        Task<bool> AddStoryAsync(int userId, Story story);
        Task<bool> SaveChangesAsync();

    }
}
