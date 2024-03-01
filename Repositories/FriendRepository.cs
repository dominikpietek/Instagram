using Instagram.Databases;
using Instagram.Interfaces;
using Instagram.Models;
using Instagram.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Repositories
{
    public class FriendRepository : IFriendRepository
    {
        private readonly InstagramDbContext _db;
        public FriendRepository(InstagramDbContext db)
        {
            _db = db;
        }
        public async Task<bool> AddFriendAsync(int userId)
        {
            _db.Users.Where(u => u.Id == userId).Include(u => u.Friends).First().Friends
                .Add(new Friend()
                {
                    FriendId = userId
                });
            return await SaveChanges.SaveAsync(_db);
        }

        public async Task<List<int>> GetAllUserFriendsIdAsync(int userId)
        {
            return await _db.Friends.Where(f => f.UserId == userId).Select(u => u.FriendId).ToListAsync();
        }

        public async Task<bool> RemoveFriendAsync(int userId, int friendId)
        {
            await _db.Friends.Where(f => f.UserId == userId).ExecuteDeleteAsync();
            await _db.Friends.Where(f => f.FriendId == friendId).ExecuteDeleteAsync();
            return await SaveChanges.SaveAsync(_db);
        }
    }
}
