using Instagram.Databases;
using Instagram.Interfaces;
using Instagram.JSONModels;
using Instagram.MessageBoxes;
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
        private readonly Action _closeWindow;

        public LoginRepository(InstagramDbContext db, IAbstractFactory<FeedView> factory, Action CloseWindow)
        {
            _db = db;
            _factory = factory;
            _closeWindow = CloseWindow;
        }

        private async Task CloseLoginWindowAndShowMainWindow()
        {
            _factory.Create().Show();
            await Task.Delay(200);
            _closeWindow.Invoke();
        }

        private async Task<User> GetUser(string emailNickname)
        {
            IUserRepository userRepository = new UserRepository(_db);
            if (emailNickname.Contains('@'))
            {
                return await userRepository.GetUserByEmailWithoutIncludesAsync(emailNickname);
            }
            else
            {
                return await userRepository.GetUserByNicknameWithoutIncludesAsync(emailNickname);
            }
        }

        private async void MakeChangesInUserDataFile(bool rememberMe, string emailNickname)
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
        }

        public async Task AutomaticLoginAsync(string emailNickname)
        {
            IsInDatabaseRepository isInDatabase = new IsInDatabaseRepository(_db, emailNickname);
            if (isInDatabase.CheckLogin("Email or Nickname doesn't exist!"))
            {
                await CloseLoginWindowAndShowMainWindow();
            }
        }

        public async Task CheckWithDatabaseAsync(string password, string emailNickname, bool rememberMe)
        {
            IsInDatabaseRepository isInDatabase = new IsInDatabaseRepository(_db, emailNickname);
            if (isInDatabase.CheckLogin("Email or Nickname doesn't exist!"))
            {
                User user = await GetUser(emailNickname);
                if (user.Password == Hash.HashString(password))
                {
                    MakeChangesInUserDataFile(rememberMe, emailNickname);
                    await CloseLoginWindowAndShowMainWindow();
                }
                else
                {
                    CustomMessageBox.Error("Wrong password!");
                }
            }
        }
    }
}
