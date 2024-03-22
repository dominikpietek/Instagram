using Instagram.Databases;
using Instagram.Interfaces;
using Instagram.Models;
using Instagram.Services;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram.Repositories
{
    public class GotSentFriendRequestModelRepository<model> : IGotSentFriendRequestModelRepository where model : FriendRequestAbstractModel
    {
        private readonly InstagramDbContext _db;
        private readonly DbSet<model> _base;
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public GotSentFriendRequestModelRepository(InstagramDbContext db)
        {
            _db = db;
            _base = _db.Set<model>();
        }
        public async Task<bool> AddAsync(FriendRequestAbstractModel userIdModel)
        {
            try
            {
                await _base.AddAsync((userIdModel as model)!);
                return await SaveChanges.SaveAsync(_db);
            }
            catch (Exception e)
            {
                _logger.Error($"{e.Message};GotSentFriendRequestModelRepository");
                throw;
            }
        }

        public async Task<List<int>> GetAllAsync(int userId)
        {
            try
            {
                return await _base.Where(b => b.UserId == userId).Select(b => b.StoredUserId).ToListAsync();
            }
            catch (Exception e)
            {
                _logger.Error($"{e.Message};GotSentFriendRequestModelRepository");
                throw;
            }
        }

        public async Task<bool> IsRequest(int userId, int requestUserId)
        {
            try
            {
                return await _base.AnyAsync(b => (b.UserId == userId && b.StoredUserId == requestUserId));
            }
            catch (Exception e)
            {
                _logger.Error($"{e.Message};GotSentFriendRequestModelRepository");
                throw;
            }
        }

        public async Task<bool> RemoveAsync(int userId, int requestUserId)
        {
            try
            {
                _base.Remove(await _base.FirstAsync(b => (b.UserId == userId && b.StoredUserId == requestUserId)));
                return await SaveChanges.SaveAsync(_db);
            }
            catch (Exception e)
            {
                _logger.Error($"{e.Message};GotSentFriendRequestModelRepository");
                throw;
            }
        }
    }
}
