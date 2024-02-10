using Instagram.Databases;
using Instagram.JSONModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Instagram.Commands
{
    public class DarkModeCommand : CommandBase
    {
        private Action<bool> _ChangeTheme;
        private bool _isDarkMode;
        public DarkModeCommand(Action<bool> ChangeTheme)
        {
            _ChangeTheme = ChangeTheme;
        }
        public override void Execute(object parameter)
        {
            ChangeDarkModeAsync();
            _ChangeTheme.Invoke(_isDarkMode);
        }
        private async Task ChangeDarkModeAsync()
        {
            JSON<UserDataModel> userJSON = new JSON<UserDataModel>("UserData");
            UserDataModel userJSONModel = await userJSON.GetAsync<UserDataModel>();
            userJSONModel.DarkMode ^= true;
            _isDarkMode = userJSONModel.DarkMode;
            await userJSON.SaveAsync(userJSONModel);
        }
    }
}
