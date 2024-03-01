using Instagram.Databases;
using Instagram.Interfaces;
using Instagram.Models;
using Instagram.SendingEmails;
using Instagram.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Repositories
{
    public class RegisterRepository
    {
        private readonly InstagramDbContext _db;
        private readonly IUserRepository _userRepository;

        public RegisterRepository(InstagramDbContext db)
        {
            _db = db;
            _userRepository = new UserRepository(_db);
        }
        public async Task<bool> ValidateData(string firstPassword, string secondPassword, string email, string nickname)
        {
            if (EqualPasswords.Equal(firstPassword, secondPassword))
            {
                IsInDatabaseRepository isInDatabaseEmail = new IsInDatabaseRepository(_userRepository, email);
                if (await isInDatabaseEmail.CheckRegisterEmailAsync("Email is already used!"))
                {
                    IsInDatabaseRepository isInDatabaseNickname = new IsInDatabaseRepository(_userRepository, nickname);
                    if (await isInDatabaseNickname.CheckRegisterNicknameAsync("Nickname is already used!"))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public async Task AddUserAsync(User user)
        {
            await _userRepository.AddUserAsync(user);
        }
    }
}
