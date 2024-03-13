using Instagram.Databases;
using Instagram.Interfaces;
using Instagram.Models;
using Instagram.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram.Repositories
{
    public class GotSentFriendRequestModelRepository<model> : IGotSentFriendRequestModelRepository where model : FriendRequestAbstractModel
    {
        private readonly InstagramDbContext _db;
        private readonly DbSet<model> _base;

        public GotSentFriendRequestModelRepository(InstagramDbContext db)
        {
            _db = db;
            _base = _db.Set<model>();
        }
        public async Task<bool> AddAsync(FriendRequestAbstractModel userIdModel)
        {
            await _base.AddAsync((userIdModel as model)!);
            return await SaveChanges.SaveAsync(_db);
        }

        public async Task<List<int>> GetAllAsync(int userId)
        {
            return await _base.Where(b => b.UserId == userId).Select(b => b.StoredUserId).ToListAsync();
        }

        public async Task<bool> RemoveAsync(int id)
        {
            _base.Remove(await _base.FirstAsync(b => b.Id == id));
            return await SaveChanges.SaveAsync(_db);
        }
    }
}
