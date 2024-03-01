using Instagram.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Services
{
    public static class SaveChanges
    {
        public static async Task<bool> SaveAsync(InstagramDbContext db)
        {
            var save = await db.SaveChangesAsync();
            return save > 0 ? true : false;
        }
    }
}
