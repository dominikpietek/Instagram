using Instagram.Databases;
using Instagram.Interfaces;
using Instagram.Models;
using Instagram.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
            try
            {
                return _db.Stories.AsEnumerable().Where(s => s.PublicationDate.AddHours(24) > DateTime.Now).ToList();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return await _db.Stories.Include(s => s.Image).ToListAsync();
        }

        public async Task<Story> GetStoryAsync(int id)
        {
            try
            {
                return await _db.Stories.Where(s => s.Id == id).Include(s => s.Image).Include(s => s.User).FirstAsync();
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }

        public async Task<bool> RemoveStoryAsync(int id)
        {
            _db.Stories.Remove(await GetStoryAsync(id));
            return await SaveChanges.SaveAsync(_db);
        }

        public async Task<bool> UpdateStoryAsync(Story story)
        {
            _db.Stories.Update(story);
            return await SaveChanges.SaveAsync(_db);
        }

        public async Task<bool> AddStoryAsync(int userId, Story story)
        {
            _db.Users.Where(u => u.Id == userId).Include(u => u.Stories).First().Stories.Add(story);
            return await SaveChanges.SaveAsync(_db);
        }
    }
}
