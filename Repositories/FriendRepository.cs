using Instagram.Databases;
using Instagram.Interfaces;
using Instagram.Models;
using Instagram.Services;
using Microsoft.Data.SqlClient.DataClassification;
using Microsoft.EntityFrameworkCore;
using NLog;
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
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public FriendRepository(InstagramDbContext db)
        {
            _db = db;
        }

        public async Task<bool> AddFriendAsync(int userId, int friendId)
        {
            try
            {
                _db.Users.Where(u => u.Id == userId).Include(u => u.Friends).First().Friends
                    .Add(new Friend()
                    {
                        FriendId = friendId
                    });
                return await SaveChanges.SaveAsync(_db);
            }
            catch (Exception e)
            {
                _logger.Error($"{e.Message};FriendRepository");
                throw;
            }
        }

        public async Task<List<int>> GetAllUserFriendsIdAsync(int userId)
        {
            try
            {
                return await _db.Friends.Where(f => f.UserId == userId).Select(u => u.FriendId).ToListAsync();
            }
            catch (Exception e)
            {
                _logger.Error($"{e.Message};FriendRepository");
                throw;
            }
        }

        public async Task<int> GetFriendId(int userId, int friendId)
        {
            try
            {
                Friend friend = await _db.Friends.FirstAsync(f => (f.UserId == userId && f.FriendId == friendId));
                return friend.Id;
            }
            catch (Exception e)
            {
                _logger.Error($"{e.Message};FriendRepository");
                throw;
            }
        }

        public async Task<Message> GetLastMessageAsync(int friendId)
        {
            try
            {
                Friend friend = await _db.Friends.FirstAsync(f => f.Id == friendId);
                return new Message();
            }
            catch (Exception e)
            {
                _logger.Error($"{e.Message};FriendRepository");
                throw;
            }
        }

        public Task<bool> IsFriend(int userId, int friendId)
        {
            try
            {
                return _db.Friends.AnyAsync(f => (f.UserId == userId && f.FriendId == friendId));
            }
            catch (Exception e)
            {
                _logger.Error($"{e.Message};FriendRepository");
                throw;
            }
        }

        public async Task<Friend> GetFriendAsync(int userId, int friendId)
        {
            try
            {
                return await _db.Friends.FirstAsync(f => f.UserId == userId && f.FriendId == friendId);
            }
            catch (Exception e)
            {
                _logger.Error($"{e.Message};FriendRepository");
                throw;
            }
        }

        public async Task<bool> RemoveFriendAsync(int userId, int friendId)
        {
            try
            {
                _db.Friends.Remove(await GetFriendAsync(userId, friendId));
                _db.Friends.Remove(await GetFriendAsync(friendId, userId));
                return await SaveChanges.SaveAsync(_db);
            }
            catch (Exception e)
            {
                _logger.Error($"{e.Message};FriendRepository");
                throw;
            }
        }
    }
}
