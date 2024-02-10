using Instagram.Databases;
using Instagram.JSONModels;
using Instagram.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Instagram.Repositories
{
    public static class LogoutRepository
    {
        public static async Task RestartAutomaticLoginAsync()
        {
            JSON<UserDataModel> userJSON = new JSON<UserDataModel>("UserData");
            UserDataModel userJSONModel = await userJSON.GetAsync<UserDataModel>();
            userJSONModel.LastLogin = DateTime.MinValue;
            await userJSON.SaveAsync(userJSONModel);
        }
        public static void CloseWindowAndShowStartUpWindow(Action CloseWindow)
        {
            Window startUpWindow = new LoginOrRegisterWindowView();
            startUpWindow.Show();
            CloseWindow.Invoke();
        }
    }
}
