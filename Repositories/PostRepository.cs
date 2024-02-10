using Instagram.Databases;
using Instagram.Interfaces;
using Instagram.Models;
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
            return await SaveChangesAsync();
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
        public async Task<bool> RemovePostByIdAsync(int postId)
        {
            var post = await _db.Posts.FirstAsync(p => p.Id == postId);
            _db.Posts.Remove(post);
            return await SaveChangesAsync();
        }

        public async Task<bool> UpdatePostAsync(Post post)
        {
            _db.Posts.Update(post);
            return await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            var save = await _db.SaveChangesAsync();
            return save > 0 ? true : false;
        }
    }
}
