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
        private readonly InstagramDbContext _db;
        private readonly string _emailNickname;
        private readonly IUserRepository _userRepository;
        public IsInDatabaseRepository(InstagramDbContext db, string emailNickname)
        {
            _db = db;
            _emailNickname = emailNickname;
            _userRepository = new UserRepository(db);
        }
        private bool Email()
        {
            try
            {
                _userRepository.GetUserByEmailWithoutIncludesAsync(_emailNickname);
                return false;
            }
            catch (Exception)
            {
                return true;
            }
        }
        private bool Nickname()
        {
            try
            {
                _userRepository.GetUserByNicknameWithoutIncludesAsync(_emailNickname);
                return false;
            }
            catch (Exception)
            {
                return true;
            }
        }
        public bool CheckLogin(string errorMessage)
        {
            if (!Email() || !Nickname())
            {
                return true;
            }
            else
            {
                CustomMessageBox.Error(errorMessage);
                return false;
            }
        }
        public bool CheckRegisterNickname(string errorMessage)
        {
            if (Nickname())
            {
                return true;
            }
            else
            {
                CustomMessageBox.Error(errorMessage);
                return false;
            }
        }
        public bool CheckRegisterEmail(string errorMessage)
        {
            if (Email())
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
