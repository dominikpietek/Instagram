using Instagram.Databases;
using Instagram.JSONModels;
using Instagram.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Instagram.Services
{
    public static class Logout
    {
        public static void RestartAutomaticLogin()
        {
            JSON<UserDataModel> userJSON = new JSON<UserDataModel>("UserData");
            UserDataModel userJSONModel = userJSON.Get<UserDataModel>();
            userJSONModel.LastLogin = DateTime.MinValue;
            userJSON.Save(userJSONModel);
        }
        public static void CloseWindowAndShowStartUpWindow(Action CloseWindow)
        {
            Window startUpWindow = new LoginOrRegisterWindowView();
            startUpWindow.Show();
            CloseWindow.Invoke();
        }
    }
}
