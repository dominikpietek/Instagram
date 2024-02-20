using Instagram.Databases;
using Instagram.JSONModels;
using Instagram.Models;
using Instagram.SendingEmails;
using Instagram.Services;
using Instagram.StartupHelpers;
using Instagram.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Instagram.Repositories
{
    public class LoginRepository
    {
        private readonly InstagramDbContext _db;
        private readonly IAbstractFactory<FeedView> _factory;

        public LoginRepository(InstagramDbContext db, IAbstractFactory<FeedView> factory)
        {
            _db = db;
            _factory = factory;
        }
        public async Task AutomaticLoginAsync(string emailNickname, Action CloseWindow, Action<bool> ChangeTheme)
        {
            IsInDatabaseRepository isInDatabase = new IsInDatabaseRepository(_db, emailNickname);
            if (isInDatabase.CheckLogin("Email or Nickname doesn't exist!"))
            {
                User user = GetUser(emailNickname);
                JSON<UserDataModel> userJSON = new JSON<UserDataModel>("UserData");
                UserDataModel userJSONModel = await userJSON.GetAsync<UserDataModel>();
                userJSONModel.RememberedEmailNickname = emailNickname;
                userJSONModel.LastLogin = DateTime.Now;
                userJSONModel.UserId = user.Id;
                await userJSON.SaveAsync(userJSONModel);
                //_factory.CreateFeed(user, ChangeTheme).Show();
                _factory.Create().Show();
                CloseWindow.Invoke();
            }
        }
        public async Task CheckWithDatabaseAsync(string password, string emailNickname, Action CloseWindow, bool rememberMe, Action<bool> ChangeTheme)
        {
            IsInDatabaseRepository isInDatabase = new IsInDatabaseRepository(_db, emailNickname);
            if (isInDatabase.CheckLogin("Email or Nickname doesn't exist!"))
            {
                User user = GetUser(emailNickname);
                if (user.Password == Hash.HashString(password))
                {
                    JSON<UserDataModel> userJSON = new JSON<UserDataModel>("UserData");
                    UserDataModel userJSONModel = await userJSON.GetAsync<UserDataModel>();
                    if (rememberMe)
                    {
                        userJSONModel.RememberedEmailNickname = emailNickname;
                        userJSONModel.LastLogin = DateTime.Now;
                    }
                    else
                    {
                        userJSONModel.RememberedEmailNickname = string.Empty;
                    }
                    await userJSON.SaveAsync(userJSONModel);
                    //_factory.CreateFeed(user, ChangeTheme).Show();
                    _factory.Create().Show();
                    CloseWindow.Invoke();
                }
                else
                {
                    MessageBox.Show(
                        "Wrong password!", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private User GetUser(string emailNickname)
        {
            if (emailNickname.Contains('@'))
            {
                return  _db.Users.First(u => u.EmailAdress == emailNickname);
            }
            else
            {
                return _db.Users.First(u => u.Nickname == emailNickname);
            }
        }
    }
}
