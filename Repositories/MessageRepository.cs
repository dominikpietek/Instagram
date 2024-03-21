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

namespace Instagram.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly InstagramDbContext _db;

        public MessageRepository(InstagramDbContext db)
        {
            _db = db;
        }
        public async Task<bool> AddMessage(Message message)
        {
            try
            {
                _db.Messages.Add(message);
                return await SaveChanges.SaveAsync(_db);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Message>> GetUserMessagesToFriend(int userId, int friendId)
        {
            return await _db.Messages.Where(m => m.UserId == userId && m.FriendId == friendId).ToListAsync();
        }

        public async Task<bool> RemoveMessage(Message message)
        {
            _db.Messages.Remove(message);
            return await SaveChanges.SaveAsync(_db);
        }

        public async Task<bool> UpdateMessage(Message message)
        {
            _db.Update(message);
            return await SaveChanges.SaveAsync(_db);
        }
    }
}
