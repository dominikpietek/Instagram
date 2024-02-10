using Instagram.Databases;
using Instagram.Interfaces;
using Instagram.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Repositories
{
    public class StoryRepository : IStoryRepository
    {
        private readonly InstagramDbContext _db;
        public StoryRepository(InstagramDbContext db)
        {
            _db = db;
        }
        public async Task<List<Story>> GetAllStoriesAsync()
        {
            return await _db.Stories.Include(s => s.Image).ToListAsync();
        }

        public async Task<Story> GetStoryAsync(int id)
        {
            return await _db.Stories.Where(s => s.Id == id).Include(s => s.Image).FirstAsync();
        }

        public async Task<bool> RemoveStoryAsync(int id)
        {
            await _db.Stories.Where(s => s.Id == id).ExecuteDeleteAsync();
            return await SaveChangesAsync();
        }

        public async Task<bool> UpdateStoryAsync(Story story)
        {
            _db.Stories.Update(story);
            return await SaveChangesAsync();
        }

        public async Task<bool> AddStoryAsync(int userId, Story story)
        {
            _db.Users.Where(u => u.Id == userId).Include(u => u.Stories).First().Stories.Add(story);
            return await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            var save = await _db.SaveChangesAsync();
            return save > 0 ? true : false;
        }
    }
}
