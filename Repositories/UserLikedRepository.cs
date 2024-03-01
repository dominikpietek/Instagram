using Instagram.Databases;
using Instagram.Enums;
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
    public class UserLikedRepository : IUserLikedRepository
    {
        private readonly InstagramDbContext _db;

        public UserLikedRepository(InstagramDbContext db)
        {
            _db = db;
        }

        public async Task<bool> AddLikeAsync(UserLiked userLiked)
        {
            await _db.UsersLiked.AddAsync(userLiked);
            return await SaveChanges.SaveAsync(_db);
        }

        public async Task<bool> IsLikedBy(int userThatLikedId, LikedThingsEnum likedThing, int likedThingId)
        {
            return await _db.UsersLiked.AnyAsync(ul => ul.UserThatLikedId == userThatLikedId && (int)ul.LikedThing == (int)likedThing && ul.LikedThingId == likedThingId);
        }

        public async Task<bool> RemoveLikeAsync(int userThatLikedId, LikedThingsEnum likedThing, int likedThingId)
        {
            _db.UsersLiked.Remove(await _db.UsersLiked.FirstAsync(ul =>
                ul.UserThatLikedId == userThatLikedId && 
                (int)ul.LikedThing == (int)likedThing && 
                ul.LikedThingId == likedThingId));
            return await SaveChanges.SaveAsync(_db);
        }
    }
}
