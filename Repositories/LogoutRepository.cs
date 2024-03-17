using Instagram.Databases;
using Instagram.JSONModels;
using Instagram.StartupHelpers;
using Instagram.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Instagram.Repositories
{
    public class LogoutRepository
    {
        public async Task RestartAutomaticLoginAsync()
        {
            JSON<UserDataModel> userJSON = new JSON<UserDataModel>("UserData");
            UserDataModel userJSONModel = userJSON.Get<UserDataModel>();
            userJSONModel.LastLogin = DateTime.MinValue;
            userJSONModel.UserId = 0;
            userJSON.Save(userJSONModel);
        }
        public void CloseWindowAndShowStartUpWindow(Action CloseWindow, IAbstractFactory<LoginOrRegisterWindowView> loginFactory)
        {
            loginFactory.Create().Show();
            CloseWindow.Invoke();
        }
    }
}
