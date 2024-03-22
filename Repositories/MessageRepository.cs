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

namespace Instagram.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly InstagramDbContext _db;
        private static Logger _logger = LogManager.GetCurrentClassLogger();

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
                _logger.Error($"{e.Message};MessageRepository");
                throw;
            }
        }

        public async Task<List<Message>> GetUserMessagesToFriend(int userId, int friendId)
        {
            try
            {
                return await _db.Messages.Where(m => m.UserId == userId && m.FriendId == friendId).ToListAsync();
            }
            catch (Exception e)
            {
                _logger.Error($"{e.Message};MessageRepository");
                throw;
            }
        }

        public async Task<bool> RemoveMessage(Message message)
        {
            try
            {
                _db.Messages.Remove(message);
                return await SaveChanges.SaveAsync(_db);
            }
            catch (Exception e)
            {
                _logger.Error($"{e.Message};MessageRepository");
                throw;
            }
        }

        public async Task<bool> UpdateMessage(Message message)
        {
            try
            {
                _db.Update(message);
                return await SaveChanges.SaveAsync(_db);
            }
            catch (Exception e)
            {
                _logger.Error($"{e.Message};MessageRepository");
                throw;
            }
        }
    }
}
