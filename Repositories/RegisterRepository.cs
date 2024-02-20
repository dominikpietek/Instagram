using Instagram.Databases;
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
        private InstagramDbContext _db;
        public RegisterRepository(InstagramDbContext db)
        {
            _db = db;
        }
        public bool ValidateData(string firstPassword, string secondPassword, string email, string nickname)
        {
            if (EqualPasswords.Equal(firstPassword, secondPassword))
            {
                IsInDatabaseRepository isInDatabaseEmail = new IsInDatabaseRepository(_db, email);
                if (isInDatabaseEmail.CheckRegisterEmail("Email is already used!"))
                {
                    IsInDatabaseRepository isInDatabaseNickname = new IsInDatabaseRepository(_db, nickname);
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
