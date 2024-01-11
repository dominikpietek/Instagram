using Instagram.Databases;
using Instagram.Models;
using Instagram.SendingEmails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Services
{
    public class Register
    {
        private InstagramDbContext _db;
        public Register()
        {
            _db = new InstagramDbContext();
        }
        public bool ValidateData(string firstPassword, string secondPassword, string email, string nickname)
        {
            if (EqualPasswords.Equal(firstPassword, secondPassword))
            {
                IsInDatabase isInDatabaseEmail = new IsInDatabase(_db, email);
                if (isInDatabaseEmail.CheckRegisterEmail("Email is already used!"))
                {
                    IsInDatabase isInDatabaseNickname = new IsInDatabase(_db, nickname);
                    if (isInDatabaseNickname.CheckRegisterNickname("Nickname is already used!"))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public void AddUser(User user)
        {
            _db.Users.Add(user);
            _db.SaveChangesAsync();
            _db.DisposeAsync();
        }
    }
}
