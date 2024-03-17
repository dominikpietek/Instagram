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
        private Func<bool, Task> _ChangeTheme;
        private bool _isDarkMode;
        public DarkModeCommand(Func<bool, Task> ChangeTheme)
        {
            _ChangeTheme = ChangeTheme;
        }
        public override void Execute(object parameter)
        {
            ChangeDarkMode();
            _ChangeTheme.Invoke(_isDarkMode);
        }
        private void ChangeDarkMode()
        {
            JSON<UserDataModel> userJSON = new JSON<UserDataModel>("UserData");
            UserDataModel userJSONModel = userJSON.Get<UserDataModel>();
            userJSONModel.DarkMode ^= true;
            _isDarkMode = userJSONModel.DarkMode;
            userJSON.Save(userJSONModel);
        }
    }
}
