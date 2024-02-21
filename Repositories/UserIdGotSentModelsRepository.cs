using Instagram.Databases;
using Instagram.Interfaces;
using Instagram.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Repositories
{
    public class UserIdGotSentModelsRepository<model> : IUserIdGotSentModelRepository where model : UserIdAbstractModel
    {
        private readonly InstagramDbContext _db;
        private readonly DbSet<model> _base;

        public UserIdGotSentModelsRepository(InstagramDbContext db)
        {
            _db = db;
            _base = _db.Set<model>();
        }
        public async Task<bool> AddAsync(UserIdAbstractModel userIdModel)
        {
            await _base.AddAsync((userIdModel as model)!);
            return await SaveChangesAsync();
        }

        public async Task<List<int>> GetAllAsync(int userId)
        {
            return await _base.Where(b => b.UserId == userId).Select(b => b.StoredUserId).ToListAsync();
        }

        public async Task<bool> RemoveAsync(int id)
        {
            _base.Remove(await _base.FirstAsync(b => b.Id == id));
            return await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            var save = await _db.SaveChangesAsync();
            return save > 0 ? true : false;
        }
    }
}
