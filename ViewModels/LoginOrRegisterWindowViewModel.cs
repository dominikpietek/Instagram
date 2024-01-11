using Instagram.Commands;
using Instagram.Databases;
using Instagram.JSONModels;
using Instagram.Models;
using Instagram.Services;
using Newtonsoft.Json;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace Instagram.ViewModels
{
    public class LoginOrRegisterWindowViewModel : ViewModelBase
    {
        public string LogoPath { get; set; } = @"C:\Programs\Instagram\Instagram\Resources\darkLogo.png";
        private string _EmailNickname;
        public string EmailNickname 
        { 
            get { return _EmailNickname;  } 
            set 
            {
                _EmailNickname = value;
                OnPropertyChanged(nameof(EmailNickname));
            } 
        }
        private string _Password;
        public string Password 
        {
            get { return _Password; }
            set 
            { 
                _Password = value;
                OnPropertyChanged(nameof(Password));     
            } 
        }
        private bool _RememberMe;
        public bool RememberMe 
        { 
            get { return _RememberMe;  }
            set 
            {
                _RememberMe = value;
                OnPropertyChanged(nameof(RememberMe));
            } 
        }
        private int currentBoxIndex = 0;
        private const int LASTBOXINDEXNUMBER = 1;
        private Action _CloseWindow;
        public ICommand LoginButton { get; set; }
        public ICommand CreateAccountOpenWindowButton { get; set; }
        public ICommand GoToNextBoxCommand { get; set; }
        private Action _FocusOnLogin;
        private Action _FocusOnPassword;
        private Func<bool> _IsLoginButtonUsable;
        private Action<bool> _ChangeTheme;

        public LoginOrRegisterWindowViewModel(Action CloseWindow, Action FocusOnLogin, Action FocusOnPassword, Func<bool> IsLoginButtonUsable, Action<bool> ChangeTheme)
        {
            // change current box index after click
            _ChangeTheme = ChangeTheme;
            _IsLoginButtonUsable = IsLoginButtonUsable;
            _CloseWindow = CloseWindow;
            _FocusOnLogin = FocusOnLogin;
            _FocusOnPassword = FocusOnPassword;
            LoginButton = new LoginCommand(LoginClick);
            CreateAccountOpenWindowButton = new CreateAccountOpenWindowButtonCommand(_CloseWindow);
            GoToNextBoxCommand = new GoToNextBoxCommand(ChangeBoxIndex);
            JSON<UserDataModel> userJSON = new JSON<UserDataModel>("UserData");
            UserDataModel userJSONModel = userJSON.Get<UserDataModel>();
            if (userJSONModel.RememberedEmailNickname != string.Empty)
            {
                _EmailNickname = userJSONModel.RememberedEmailNickname;
                if (userJSONModel.LastLogin.AddHours(2) >= DateTime.Now)
                {
                    Login.AutomaticLogin(_EmailNickname, _CloseWindow, _ChangeTheme);
                }
                RememberMe = true;
            }
            // dark mode
            ChangeTheme.Invoke(userJSONModel.DarkMode);
        }

        public void ChangeBoxIndex()
        {
            if(currentBoxIndex + 1 > LASTBOXINDEXNUMBER)
            {
                if (_IsLoginButtonUsable.Invoke())
                {
                    object newObject = "new object";
                    LoginButton.Execute(newObject);
                }
                else
                {
                    currentBoxIndex = 0;
                }
            }
            else
            {
                currentBoxIndex++;
            }
            switch(currentBoxIndex) {
                case 0:
                    _FocusOnLogin.Invoke();
                    break;
                case 1:
                    _FocusOnPassword.Invoke();
                    break;
            }
        }
        public void LoginClick()
        {
            Login.CheckWithDatabase(_Password, _EmailNickname, _CloseWindow, RememberMe, _ChangeTheme);
        }
    }
}
