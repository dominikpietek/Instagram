using Instagram.Databases;
using Instagram.Services;
using Instagram.StartupHelpers;
using Instagram.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace Instagram.Views
{
    public partial class LoginOrRegisterWindowView : Window
    {
        public LoginOrRegisterWindowView(
            IAbstractFactory<CreateAccountWindowView> accountFactory,
            IAbstractFactory<FeedView> feedFactory,
            InstagramDbContext db)
        {
            InitializeComponent();
            ChangeTheme.ChangeAsync(this.Resources);
            FocusOnController focusOnController = new FocusOnController(this.emailNicknameBox, this.passwordBox);
            DataContext = new LoginOrRegisterWindowViewModel(
                CloseWindow,  
                focusOnController,
                IsLoginButtonUsable, 
                accountFactory, 
                feedFactory, 
                db);
        }

        public void CloseWindow()
        {
            this.Close();
        }

        public bool IsLoginButtonUsable()
        {
            return this.loginButton.IsEnabled;
        }
    }
}
