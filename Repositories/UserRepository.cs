using Instagram.Databases;
using Instagram.DTOs;
using Instagram.Interfaces;
using Instagram.Models;
using Instagram.SendingEmails;
using Instagram.Services;
using Microsoft.EntityFrameworkCore;
using NLog;
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
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public UserRepository(InstagramDbContext db)
        {
            _db = db;
        }

        public async Task<List<User>> GetAllUsersWithPhotosAndRequestsAsync()
        {
            try
            {
                return await _db.Users.Include(u => u.ProfilePhoto)
                    .Include(u => u.GotFriendRequests)
                    .Include(u => u.SentFriendRequests)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                _logger.Error($"{e.Message};UserRepository");
                throw;
            }
        }

        public async Task<User> GetUserWithPhotoAndRequestsAsync(int id)
        {
            try
            {
                return _db.Users.Where(u => u.Id == id)
                    .Include(u => u.ProfilePhoto)
                    .Include(u => u.GotFriendRequests)
                    .Include(u => u.SentFriendRequests)
                    .Include(u => u.Stories)
                    .ThenInclude(s => s.Image)
                    .First();
            }
            catch (Exception e)
            {
                _logger.Error($"{e.Message};UserRepository");
                throw;
            }
        }

        public async Task<bool> RemoveUserAsync(int id)
        {
            try
            {
                _db.Users.Remove(_db.Users.Where(u => u.Id == id).First());
                return await SaveChanges.SaveAsync(_db);
            }
            catch (Exception e)
            {
                _logger.Error($"{e.Message};UserRepository");
                throw;
            }
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            try
            {
                _db.Users.Update(user);
                return await SaveChanges.SaveAsync(_db);
            }
            catch (Exception e)
            {
                _logger.Error($"{e.Message};UserRepository");
                throw;
            }
        }

        public async Task<bool> AddUserAsync(User user)
        {
            try
            {
                await _db.Users.AddAsync(user);
                return await SaveChanges.SaveAsync(_db);
            }
            catch (Exception e)
            {
                _logger.Error($"{e.Message};UserRepository");
                throw;
            }
        }

        public async Task<List<User>> GetAllNotFriendsUsersAsync(int userId, IFriendRepository friendRepository, IGotSentFriendRequestModelRepository userIdSentModelRepository)
        {
            try
            {
                List<int> userFriendsIds = await friendRepository.GetAllUserFriendsIdAsync(userId);
                List<int> sentRequestPeople = await userIdSentModelRepository.GetAllAsync(userId);
                return await _db.Users.Where(u =>
                    u.Id != userId &&
                    !userFriendsIds.Contains(u.Id) &&
                    !sentRequestPeople.Contains(u.Id)
                    ).Include(u => u.ProfilePhoto).ToListAsync();
            }
            catch (Exception e)
            {
                _logger.Error($"{e.Message};UserRepository");
                throw;
            }
        }

        public async Task<User> GetUserByNicknameWithoutIncludesAsync(string nickname)
        {
            try
            {
                return _db.Users.First(u => u.Nickname == nickname);
            }
            catch (Exception e)
            {
                _logger.Error($"{e.Message};UserRepository");
                return new User() { Id = -1 };
            }
        }

        public async Task<User> GetUserByEmailWithoutIncludesAsync(string email)
        {
            try
            {
                return _db.Users.First(u => u.EmailAdress == email);
            }
            catch (Exception e)
            {
                _logger.Error($"{e.Message};UserRepository");
                return new User() { Id = -1 };
            }
            
        }

        public async Task<User> GetOnlyEssentialDataAsync(int id)
        {
            try
            {
                return _db.Users.First(u => u.Id == id);
            }
            catch (Exception e)
            {
                _logger.Error($"{e.Message};UserRepository");
                return new User() { Id = -1 };
            }

        }

        public async Task<List<SearchUserDto>> GetUsersIdAndNickaname()
        {
            try
            {
                return await _db.Users.Select(u => new SearchUserDto() { Id = u.Id, Nickname = u.Nickname.ToLower() }).ToListAsync();
            }
            catch (Exception e)
            {
                _logger.Error($"{e.Message};UserRepository");
                throw;
            }
        }
    }
}
