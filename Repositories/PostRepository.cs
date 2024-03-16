﻿using Instagram.Databases;
using Instagram.Interfaces;
using Instagram.Models;
using Instagram.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Repositories
{
    public class PostRepository : IPostRepository
    {
        private InstagramDbContext _db;

        public PostRepository(InstagramDbContext db)
        {
            _db = db;
        }

        public async Task<bool> AddPostAsync(Post post)
        {
            await _db.Posts.AddAsync(post);
            return await SaveChanges.SaveAsync(_db);
        }

        public async Task<List<Post>> GetAllPostsWithAllDataToShowAsync()
        {
            return await _db.Posts
            .Include(p => p.Tags)
            .Include(p => p.User)
            .ThenInclude(u => u.ProfilePhoto)
            .Include(p => p.Image)
            .Include(p => p.Comments).ToListAsync();

        }

        public async Task<Post> GetPostWithAllDataAsync(int id)
        {
            return _db.Posts
            .Include(p => p.Tags)
            .Include(p => p.User)
            .ThenInclude(u => u.ProfilePhoto)
            .Include(p => p.Image)
            .Include(p => p.Comments).First(p => p.Id == id);
        }

        public async Task<List<Post>> GetUserPostsWithAllDataToShowAsync(int userId)
        {
            return await _db.Posts.Where(p => p.UserId == userId).ToListAsync();
        }

        public async Task<bool> RemovePostByIdAsync(int postId)
        {
            var post = await _db.Posts.FirstAsync(p => p.Id == postId);
            _db.Posts.Remove(post);
            return await SaveChanges.SaveAsync(_db);
        }

        public async Task<bool> UpdatePostAsync(Post post)
        {
            _db.Posts.Update(post);
            return await SaveChanges.SaveAsync(_db);
        }
    }
}
