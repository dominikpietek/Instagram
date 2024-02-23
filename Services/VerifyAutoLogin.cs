using Instagram.Interfaces;
using Instagram.JSONModels;
using Instagram.Models;
using Instagram.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Services
{
    public class VerifyAutoLogin
    {
        private UserDataModel _userDataModel;
        public VerifyAutoLogin()
        {
            AssignPrivateUserData();
        }
        private async Task AssignPrivateUserData()
        {
            _userDataModel = await GetUser.FromFileAsync();
        }
        public async Task<bool> IsAutoLogged()
        {
            if (IsUserRemembered())
            {
                if (_userDataModel.LastLogin.AddHours(2) >= DateTime.Now)
                {
                    return true;
                }
            }
            return false;
        }
        public bool IsUserRemembered()
        {
            if (_userDataModel.RememberedEmailNickname != string.Empty)
            {
                return true;
            }
            return false;
        }
        public bool IsDarkMode()
        {
            return _userDataModel.DarkMode;
        }

        public string LoginName()
        {
            return _userDataModel.RememberedEmailNickname;
        }
    }
}
