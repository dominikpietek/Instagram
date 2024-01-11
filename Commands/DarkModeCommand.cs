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
        public DarkModeCommand(Action<bool> ChangeTheme)
        {
            _ChangeTheme = ChangeTheme;
        }
        public override void Execute(object parameter)
        {
            JSON<UserDataModel> userJSON = new JSON<UserDataModel>("UserData");
            UserDataModel userJSONModel = userJSON.Get<UserDataModel>();
            userJSONModel.DarkMode ^= true;
            userJSON.Save(userJSONModel);
            _ChangeTheme.Invoke(userJSONModel.DarkMode);
        }
    }
}
