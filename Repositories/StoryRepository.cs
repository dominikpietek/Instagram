﻿using Instagram.Databases;
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
                await _db.Stories.Include(s => s.Image).ToListAsync();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return await _db.Stories.Include(s => s.Image).ToListAsync();
        }

        public async Task<Story> GetStoryAsync(int id)
        {
            return await _db.Stories.Where(s => s.Id == id).Include(s => s.Image).FirstAsync();
        }

        public async Task<bool> RemoveStoryAsync(int id)
        {
            await _db.Stories.Where(s => s.Id == id).ExecuteDeleteAsync();
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
