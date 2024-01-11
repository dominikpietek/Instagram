using Instagram.Databases;
using Instagram.JSONModels;
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

namespace Instagram.Services
{
    public class IsInDatabase
    {
        private InstagramDbContext _db;
        private string _emailNickname;
        public IsInDatabase(InstagramDbContext db, string emailNickname)
        {
            _db = db;
            _emailNickname = emailNickname;
        }
        private bool Email()
        {
            try
            {
                User user = _db.Users.First(u => u.EmailAdress == _emailNickname);
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
                User user = _db.Users.First(u => u.Nickname == _emailNickname);
                return false;
            }
            catch (Exception)
            {
                return true;
            }
        }
        public bool CheckLogin(string errorMessage)
        {
            if(!Email() || !Nickname())
            {
                return true;
            }
            else
            {
                MessageBox.Show(
                    errorMessage, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show(
                    errorMessage, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show(
                    errorMessage, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
