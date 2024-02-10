using Instagram.Commands;
using Instagram.Databases;
using Instagram.JSONModels;
using Instagram.Models;
using Instagram.Repositories;
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
        #region Resources
        private readonly string _path;
        public string LogoPath { get; set; }
        #endregion
        #region OnPropertyChangeProperties
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
        private string _BackgroundColour;
        public string BackgroundColour
        {
            get { return _BackgroundColour; }
            set
            {
                _BackgroundColour = value;
                OnPropertyChanged(nameof(BackgroundColour));
            }
        }
        #endregion
        #region Commands
        public ICommand LoginButton { get; set; }
        public ICommand CreateAccountOpenWindowButton { get; set; }
        public ICommand GoToNextBoxCommand { get; set; }
        #endregion
        #region PrivateProperties
        private Action _CloseWindow;
        private Action _FocusOnLogin;
        private Action _FocusOnPassword;
        private Func<int> _WhichOneIsFocused;
        private Func<bool> _IsLoginButtonUsable;
        private Action<bool> _ChangeTheme;
        #endregion
        public LoginOrRegisterWindowViewModel(Action CloseWindow, Action FocusOnLogin, Action FocusOnPassword, Func<bool> IsLoginButtonUsable, Action<bool> ChangeTheme, Func<int> WhichOneIsFocused)
        {
            #region PrivatePropertiesAssignment
            _path = ConfigurationManager.AppSettings.Get("ResourcesPath");
            _ChangeTheme = ChangeTheme;
            _IsLoginButtonUsable = IsLoginButtonUsable;
            _CloseWindow = CloseWindow;
            _FocusOnLogin = FocusOnLogin;
            _FocusOnPassword = FocusOnPassword;
            _WhichOneIsFocused = WhichOneIsFocused;
            #endregion
            #region CommandInstances
            LoginButton = new LoginCommand(LoginClickAsync);
            CreateAccountOpenWindowButton = new CreateAccountOpenWindowButtonCommand(_CloseWindow);
            GoToNextBoxCommand = new GoToNextBoxCommand(ChangeBoxIndex);
            #endregion
            ReadJsonConfigFileAndApplyThemeAsync();
        }
        private async Task ReadJsonConfigFileAndApplyThemeAsync()
        {
            JSON<UserDataModel> userJSON = new JSON<UserDataModel>("UserData");
            UserDataModel userJSONModel = await userJSON.GetAsync<UserDataModel>();
            if (userJSONModel.RememberedEmailNickname != string.Empty)
            {
                _EmailNickname = userJSONModel.RememberedEmailNickname;
                if (userJSONModel.LastLogin.AddHours(2) >= DateTime.Now)
                {
                    await LoginRepository.AutomaticLoginAsync(_EmailNickname, _CloseWindow, _ChangeTheme);
                }
                RememberMe = true;
            }
            ApplyTheme(userJSONModel.DarkMode);
        }
        private void ApplyTheme(bool isDarkMode)
        {
            BackgroundColour = isDarkMode ? "#CBC8CC" : "white";
            _ChangeTheme.Invoke(isDarkMode);
            ChangeLogo(isDarkMode);
        }
        private void ChangeLogo(bool isDarkMode)
        {
            if (isDarkMode)
            {
                LogoPath = $"{_path}darkLogo.png";
            }
            else
            {
                LogoPath = $"{_path}logo.png";
            }
        }
        public void ChangeBoxIndex()
        {
            int whichOneIsFocused = _WhichOneIsFocused.Invoke();
            if (whichOneIsFocused == 0)
            {
                _FocusOnLogin();
                return;
            }
            if (whichOneIsFocused == 1) 
            {
                _FocusOnPassword();
                return;
            }
            if (whichOneIsFocused == 2)
            {
                if (_IsLoginButtonUsable.Invoke())
                {
                    LoginClickAsync();
                    return;
                }
                else
                {
                    _FocusOnLogin();
                    return;
                }
            }
        }
        public async Task LoginClickAsync()
        {
            await LoginRepository.CheckWithDatabaseAsync(_Password, _EmailNickname, _CloseWindow, RememberMe, _ChangeTheme);
        }
    }
}
