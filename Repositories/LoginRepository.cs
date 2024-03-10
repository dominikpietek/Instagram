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
        private readonly IUserRepository _userRepository;

        public LoginRepository(InstagramDbContext db, IAbstractFactory<FeedView> factory, Action CloseWindow)
        {
            _db = db;
            _factory = factory;
            _closeWindow = CloseWindow;
            _userRepository = new UserRepository(_db);
        }

        private void CloseLoginWindowAndShowMainWindow()
        {
            _factory.Create().Show();
            _closeWindow.Invoke();
        }

        private async Task<User> GetUser(string emailNickname, IUserRepository userRepository)
        {
            if (emailNickname.Contains('@'))
            {
                return await userRepository.GetUserByEmailWithoutIncludesAsync(emailNickname);
            }
            else
            {
                return await userRepository.GetUserByNicknameWithoutIncludesAsync(emailNickname);
            }
        }

        private async void MakeChangesInUserDataFile(bool rememberMe, string emailNickname, int userId)
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
            userJSONModel.UserId = userId;
            await userJSON.SaveAsync(userJSONModel);
        }

        public async Task AutomaticLoginAsync(string emailNickname)
        {
            IsInDatabaseRepository isInDatabase = new IsInDatabaseRepository(_userRepository, emailNickname);
            if (await isInDatabase.CheckLoginAsync("Email or Nickname doesn't exist!"))
            {
                CloseLoginWindowAndShowMainWindow();
            }
        }

        public async Task CheckWithDatabaseAsync(string password, string emailNickname, bool rememberMe)
        {
            IsInDatabaseRepository isInDatabase = new IsInDatabaseRepository(_userRepository, emailNickname);
            if (await isInDatabase.CheckLoginAsync("Email or Nickname doesn't exist!"))
            {
                User user = await GetUser(emailNickname, isInDatabase.userRepository);
                if (user.Password == Hash.HashString(password))
                {
                    MakeChangesInUserDataFile(rememberMe, emailNickname, user.Id);
                    CloseLoginWindowAndShowMainWindow();
                }
                else
                {
                    CustomMessageBox.Error("Wrong password!");
                }
            }
        }
    }
}
