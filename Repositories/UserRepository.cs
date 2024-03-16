using Instagram.Databases;
using Instagram.DTOs;
using Instagram.Interfaces;
using Instagram.Models;
using Instagram.SendingEmails;
using Instagram.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
            return _db.Users.Where(u => u.Id == id)
                .Include(u => u.ProfilePhoto)
                .Include(u => u.GotFriendRequests)
                .Include(u => u.SentFriendRequests)
                .Include(u => u.Stories)
                .ThenInclude(s => s.Image)
                .First();
        }

        public async Task<bool> RemoveUserAsync(int id)
        {
            await _db.Users.Where(u => u.Id == id).ExecuteDeleteAsync();
            return await SaveChanges.SaveAsync(_db);
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            _db.Users.Update(user);
            return await SaveChanges.SaveAsync(_db);
        }

        public async Task<bool> AddUserAsync(User user)
        {
            await _db.Users.AddAsync(user);
            return await SaveChanges.SaveAsync(_db);
        }

        public async Task<List<User>> GetAllNotFriendsUsersAsync(int userId, IFriendRepository friendRepository, IGotSentFriendRequestModelRepository userIdSentModelRepository)
        {
            List<int> userFriendsIds = await friendRepository.GetAllUserFriendsIdAsync(userId);
            List<int> sentRequestPeople = await userIdSentModelRepository.GetAllAsync(userId);
            return await _db.Users.Where(u =>
                u.Id != userId &&
                !userFriendsIds.Contains(u.Id) &&
                !sentRequestPeople.Contains(u.Id)
                ).Include(u => u.ProfilePhoto).ToListAsync();
        }

        public async Task<User> GetUserByNicknameWithoutIncludesAsync(string nickname)
        {
            try
            {
                return await _db.Users.FirstAsync(u => u.Nickname == nickname);
            }
            catch (Exception)
            {
                return new User() { Id = -1 };
            }
            
        }

        public async Task<User> GetUserByEmailWithoutIncludesAsync(string email)
        {
            try
            {
                return await _db.Users.FirstAsync(u => u.EmailAdress == email);
            }
            catch (Exception)
            {
                return new User() { Id = -1 };
            }
            
        }

        public async Task<User> GetOnlyEssentialDataAsync(int id)
        {
            try
            {
                return await _db.Users.FirstAsync(u => u.Id == id);
            }
            catch (Exception e)
            {
                throw new Exception();
            }
            
        }

        public async Task<List<SearchUserDto>> GetUsersIdAndNickaname()
        {
            return await _db.Users.Select(u => new SearchUserDto() { Id = u.Id, Nickname = u.Nickname.ToLower() }).ToListAsync();
        }
    }
}
