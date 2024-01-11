using Instagram.Databases;
using Instagram.JSONModels;
using Instagram.Models;
using Instagram.SendingEmails;
using Instagram.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Instagram.Services
{
    public static class Login
    {
        public static void AutomaticLogin(string emailNickname, Action CloseWindow, Action<bool> ChangeTheme)
        {
            using (var db = new InstagramDbContext())
            {
                IsInDatabase isInDatabase = new IsInDatabase(db, emailNickname);
                if (isInDatabase.CheckLogin("Email or Nickname doesn't exist!"))
                {
                    User user;
                    if (emailNickname.Contains('@'))
                    {
                        user = db.Users.First(u => u.EmailAdress == emailNickname);
                    }
                    else
                    {
                        user = db.Users.First(u => u.Nickname == emailNickname);
                    }
                    JSON<UserDataModel> userJSON = new JSON<UserDataModel>("UserData");
                    UserDataModel userJSONModel = userJSON.Get<UserDataModel>();
                    userJSONModel.RememberedEmailNickname = emailNickname;
                    userJSONModel.LastLogin = DateTime.Now;
                    userJSON.Save(userJSONModel);
                    Window feedWindow = new FeedView(user, ChangeTheme);
                    feedWindow.Show();
                    CloseWindow.Invoke();
                }
            }
        }
        public static void CheckWithDatabase(string password, string emailNickname, Action CloseWindow, bool rememberMe, Action<bool> ChangeTheme)
        {
            using (var db = new InstagramDbContext())
            {
                IsInDatabase isInDatabase = new IsInDatabase(db, emailNickname);
                if (isInDatabase.CheckLogin("Email or Nickname doesn't exist!"))
                {
                    User user;
                    if (emailNickname.Contains('@'))
                    {
                        user = db.Users.First(u => u.EmailAdress == emailNickname);
                    }
                    else
                    {
                        user = db.Users.First(u => u.Nickname == emailNickname);
                    }
                    if (user.Password == Hash.HashString(password))
                    {
                        JSON<UserDataModel> userJSON = new JSON<UserDataModel>("UserData");
                        UserDataModel userJSONModel = userJSON.Get<UserDataModel>();
                        if (rememberMe)
                        {
                            userJSONModel.RememberedEmailNickname = emailNickname;
                            userJSONModel.LastLogin = DateTime.Now;
                        }
                        else
                        {
                            userJSONModel.RememberedEmailNickname = string.Empty;
                        }
                        userJSON.Save(userJSONModel);
                        Window feedWindow = new FeedView(user, ChangeTheme);
                        feedWindow.Show();
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
        }
    }
}
