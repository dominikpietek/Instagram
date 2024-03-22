using Instagram.Databases;
using Instagram.Enums;
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
    public class UserLikedRepository : IUserLikedRepository
    {
        private readonly InstagramDbContext _db;
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public UserLikedRepository(InstagramDbContext db)
        {
            _db = db;
        }

        public async Task<bool> AddLikeAsync(UserLiked userLiked)
        {
            try
            {
                await _db.UsersLiked.AddAsync(userLiked);
                return await SaveChanges.SaveAsync(_db);
            }
            catch (Exception e)
            {
                _logger.Error($"{e.Message};UserLikedRepository");
                throw;
            }
        }

        public async Task<bool> IsLikedBy(int userThatLikedId, LikedThingsEnum likedThing, int likedThingId)
        {
            try
            {
                return await _db.UsersLiked.AnyAsync(ul => ul.UserThatLikedId == userThatLikedId && (int)ul.LikedThing == (int)likedThing && ul.LikedThingId == likedThingId);
            }
            catch (Exception e)
            {
                _logger.Error($"{e.Message};UserLikedRepository");
                throw;
            }
        }

        public async Task<bool> RemoveLikeAsync(int userThatLikedId, LikedThingsEnum likedThing, int likedThingId)
        {
            try
            {
                _db.UsersLiked.Remove(await _db.UsersLiked.FirstAsync(ul =>
                    ul.UserThatLikedId == userThatLikedId &&
                    (int)ul.LikedThing == (int)likedThing &&
                    ul.LikedThingId == likedThingId));
                return await SaveChanges.SaveAsync(_db);
            }
            catch (Exception e)
            {
                _logger.Error($"{e.Message};UserLikedRepository");
                throw;
            }
        }
    }
}
