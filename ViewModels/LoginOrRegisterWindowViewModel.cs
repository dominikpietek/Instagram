using Instagram.Commands;
using Instagram.Databases;
using Instagram.JSONModels;
using Instagram.Models;
using Instagram.Repositories;
using Instagram.Services;
using Instagram.StartupHelpers;
using Instagram.Views;
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
        private FocusOnController _focusOnController;
        private Func<bool> _IsLoginButtonUsable;
        private IAbstractFactory<CreateAccountWindowView> _accountFactory;
        private IAbstractFactory<FeedView> _feedFactory;
        private InstagramDbContext _db;
        private LoginRepository _loginRepository;
        #endregion
        public LoginOrRegisterWindowViewModel(
            Action CloseWindow, 
            FocusOnController focusOnController,
            Func<bool> IsLoginButtonUsable,
            IAbstractFactory<CreateAccountWindowView> accountFactory,
            IAbstractFactory<FeedView> feedFactory,
            InstagramDbContext db)
        {
            #region PrivatePropertiesAssignment
            _path = ConfigurationManager.AppSettings.Get("ResourcesPath")!;
            _IsLoginButtonUsable = IsLoginButtonUsable;
            _CloseWindow = CloseWindow;
            _focusOnController = focusOnController;
            _accountFactory = accountFactory;
            _feedFactory = feedFactory;
            _db = db;
            _loginRepository = new LoginRepository(_db, _feedFactory, _CloseWindow);
            #endregion
            #region CommandInstances
            LoginButton = new LoginCommand(LoginClickAsync);
            CreateAccountOpenWindowButton = new CreateAccountOpenWindowButtonCommand(_CloseWindow, _accountFactory);
            GoToNextBoxCommand = new GoToNextBoxCommand(ChangeBoxIndex);
            #endregion
            Init();
        }

        private async Task Init()
        {
            VerifyAutoLogin verifyAutoLogin = new VerifyAutoLogin();

            // check auto login
            if (await verifyAutoLogin.IsAutoLogged())
            {
                await _loginRepository.AutomaticLoginAsync(verifyAutoLogin.LoginName());
            }

            // assign properties
            EmailNickname = verifyAutoLogin.LoginName();
            RememberMe = verifyAutoLogin.IsUserRemembered();
            LogoPath = ChangeTheme.ChangeLogo(_path, verifyAutoLogin.IsDarkMode());
        }

        public void ChangeBoxIndex()
        {
            bool loginOrNot = _focusOnController.WantToLoginOrChangeBoxIndex(_IsLoginButtonUsable);
            if (loginOrNot)
            {
                LoginClickAsync();
            }
        }

        public async Task LoginClickAsync()
        {
            await _loginRepository.CheckWithDatabaseAsync(_Password, _EmailNickname, RememberMe);
        }
    }
}
