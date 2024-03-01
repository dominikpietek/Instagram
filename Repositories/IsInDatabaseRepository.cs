using Instagram.Databases;
using Instagram.Interfaces;
using Instagram.JSONModels;
using Instagram.MessageBoxes;
using Instagram.Models;
using Instagram.SendingEmails;
using Instagram.Validations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Instagram.Repositories
{
    public class IsInDatabaseRepository
    {
        private readonly string _emailNickname;
        public IUserRepository userRepository;
        public IsInDatabaseRepository(IUserRepository userRepository, string emailNickname)
        {
            _emailNickname = emailNickname;
            this.userRepository = userRepository;
        }
        private bool ReturnFalseIfUserExists(User user)
        {
            if (user.Id == -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private async Task<bool> EmailAsync()
        {
            User user = await userRepository.GetUserByEmailWithoutIncludesAsync(_emailNickname);
            return ReturnFalseIfUserExists(user);
        }
        private async Task<bool> NicknameAsync()
        {
            User user = await userRepository.GetUserByNicknameWithoutIncludesAsync(_emailNickname);
            return ReturnFalseIfUserExists(user);
        }
        public async Task<bool> CheckLoginAsync(string errorMessage)
        {
            if (!await EmailAsync() || !await NicknameAsync())
            {
                return true;
            }
            else
            {
                CustomMessageBox.Error(errorMessage);
                return false;
            }
        }
        public async Task<bool> CheckRegisterNicknameAsync(string errorMessage)
        {
            if (await NicknameAsync())
            {
                return true;
            }
            else
            {
                CustomMessageBox.Error(errorMessage);
                return false;
            }
        }
        public async Task<bool> CheckRegisterEmailAsync(string errorMessage)
        {
            if (await EmailAsync())
            {
                return true;
            }
            else
            {
                CustomMessageBox.Error(errorMessage);
                return false;
            }
        }
    }
}
