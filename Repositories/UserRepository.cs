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
    public class UserRepository : IUserRepository
    {
        private readonly InstagramDbContext _db;
        public UserRepository(InstagramDbContext db)
        {
            _db = db;
        }
        public async Task<List<User>> GetAllUsersWithPhotosAndRequestsAsync()
        {
            return await _db.Users.Include(u => u.ProfilePhoto)
                .Include(u => u.GotFriendRequests)
                .Include(u => u.SentFriendRequests)
                .ToListAsync();
        }

        public async Task<User> GetUserWithPhotoAndRequestsAsync(int id)
        {
            return await _db.Users.Where(u => u.Id == id)
                .Include(u => u.ProfilePhoto)
                .Include(u => u.GotFriendRequests)
                .Include(u => u.SentFriendRequests)
                .FirstAsync();
        }

        public async Task<bool> RemoveUserAsync(int id)
        {
            await _db.Users.Where(u => u.Id == id).ExecuteDeleteAsync();
            return await SaveChangesAsync();
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            _db.Users.Update(user);
            return await SaveChangesAsync();
        }

        public async Task<bool> AddUserAsync(User user)
        {
            await _db.Users.AddAsync(user);
            return await SaveChangesAsync();
        }

        public async Task<List<User>> GetAllNotFriendsUsersAsync(int userId, IFriendRepository friendRepository, IUserIdGotSentModelRepository userIdSentModelRepository)
        {
            List<int> userFriendsIds = await friendRepository.GetAllUserFriendsIdAsync(userId);
            List<int> sentRequestPeople = await userIdSentModelRepository.GetAllAsync(userId);
            return await _db.Users.Where(u =>
                u.Id != userId &&
                !userFriendsIds.Contains(u.Id) &&
                !sentRequestPeople.Contains(u.Id)
                ).Include(u => u.ProfilePhoto).ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            var save = await _db.SaveChangesAsync();
            return save > 0 ? true : false;
        }

        public async Task<User> GetUserByNicknameWithoutIncludesAsync(string nickname)
        {
            return await _db.Users.FirstAsync(u => u.Nickname == nickname);
        }

        public async Task<User> GetUserByEmailWithoutIncludesAsync(string email)
        {
            return await _db.Users.FirstAsync(u => u.EmailAdress == email);
        }
    }
}
