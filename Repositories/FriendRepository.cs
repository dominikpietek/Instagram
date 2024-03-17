using Instagram.Databases;
using Instagram.Interfaces;
using Instagram.Models;
using Instagram.Services;
using Microsoft.Data.SqlClient.DataClassification;
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
        public async Task<bool> AddFriendAsync(int userId, int friendId)
        {
            _db.Users.Where(u => u.Id == userId).Include(u => u.Friends).First().Friends
                .Add(new Friend()
                {
                    FriendId = friendId
                });
            return await SaveChanges.SaveAsync(_db);
        }

        public async Task<List<int>> GetAllUserFriendsIdAsync(int userId)
        {
            return await _db.Friends.Where(f => f.UserId == userId).Select(u => u.FriendId).ToListAsync();
        }

        public async Task<int> GetFriendId(int userId, int friendId)
        {
            Friend friend = await _db.Friends.FirstAsync(f => (f.UserId == userId && f.FriendId == friendId));
            return friend.Id;
        }

        public async Task<Message> GetLastMessageAsync(int friendId)
        {
            Friend friend = await _db.Friends.FirstAsync(f => f.Id == friendId);
            try
            {
                return friend.Messages.Last();
            }
            catch (Exception)
            {

                return null;
            }
            
        }

        public Task<bool> IsFriend(int userId, int friendId)
        {
            return _db.Friends.AnyAsync(f => (f.UserId == userId && f.FriendId == friendId));
        }

        public async Task<bool> RemoveFriendAsync(int userId, int friendId)
        {
            await _db.Friends.Where(f => f.UserId == userId).ExecuteDeleteAsync();
            await _db.Friends.Where(f => f.FriendId == friendId).ExecuteDeleteAsync();
            return await SaveChanges.SaveAsync(_db);
        }
    }
}
