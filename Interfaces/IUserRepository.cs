using Instagram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllNotFriendsUsersAsync(int userId, IFriendRepository friendRepository, IUserIdGotSentModelRepository userIdSentModelRepository);
        Task<List<User>> GetAllUsersWithPhotosAndRequestsAsync();
        Task<User> GetUserWithPhotoAndRequestsAsync(int id);
        Task<bool> AddUserAsync(User user);
        Task<bool> RemoveUserAsync(int id);
        Task<bool> UpdateUserAsync(User user);
        Task<User> GetUserByNicknameWithoutIncludesAsync(string nickname);
        Task<User> GetUserByEmailWithoutIncludesAsync(string email);
        Task<User> GetOnlyEssentialDataAsync(int id);
    }
}
