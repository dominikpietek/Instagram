using Instagram.Databases;
using Instagram.Interfaces;
using Instagram.Models;
using Instagram.Services;
using Microsoft.EntityFrameworkCore;
using NLog;
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
        private static Logger _logger = LogManager.GetCurrentClassLogger();

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
                _logger.Error($"{e.Message};StoryRepository");
                throw;
            }
        }

        public async Task<Story> GetStoryAsync(int id)
        {
            try
            {
                return await _db.Stories.Where(s => s.Id == id).Include(s => s.Image).Include(s => s.User).FirstAsync();
            }
            catch (Exception e)
            {
                _logger.Error($"{e.Message};StoryRepository");
                throw;
            }
        }

        public async Task<bool> RemoveStoryAsync(int id)
        {
            try
            {
                _db.Stories.Remove(await GetStoryAsync(id));
                return await SaveChanges.SaveAsync(_db);
            }
            catch (Exception e)
            {
                _logger.Error($"{e.Message};StoryRepository");
                throw;
            }
        }

        public async Task<bool> UpdateStoryAsync(Story story)
        {
            try
            {
                _db.Stories.Update(story);
                return await SaveChanges.SaveAsync(_db);
            }
            catch (Exception e)
            {
                _logger.Error($"{e.Message};StoryRepository");
                throw;
            }
        }

        public async Task<bool> AddStoryAsync(int userId, Story story)
        {
            try
            {
                _db.Users.Where(u => u.Id == userId).Include(u => u.Stories).First().Stories.Add(story);
                return await SaveChanges.SaveAsync(_db);
            }
            catch (Exception e)
            {
                _logger.Error($"{e.Message};StoryRepository");
                throw;
            }
        }
    }
}
