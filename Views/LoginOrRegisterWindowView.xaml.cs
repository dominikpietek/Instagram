using Instagram.Databases;
using Instagram.StartupHelpers;
using Instagram.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Instagram.Views
{
    /// <summary>
    /// Interaction logic for LoginOrRegisterWindowView.xaml
    /// </summary>
    public partial class LoginOrRegisterWindowView : Window
    {
        public LoginOrRegisterWindowView(
            IAbstractFactory<CreateAccountWindowView> accountFactory,
            IAbstractFactory<FeedView> feedFactory,
            InstagramDbContext db)
        {
            InitializeComponent();
            DataContext = new LoginOrRegisterWindowViewModel(CloseWindow, FocusOnLogin, FocusOnPassword, IsLoginButtonUsable, ChangeLoginTheme, WhichOneIsFocused, accountFactory, feedFactory, db);
        }
        private void CloseWindow()
        {
            this.Close();
        }
        private int WhichOneIsFocused()
        {
            if (!this.emailNicknameBox.IsKeyboardFocusWithin && !this.passwordBox.IsKeyboardFocusWithin)
            {
                return 0;
            }
            return this.emailNicknameBox.IsKeyboardFocusWithin ? 1 : 2;
        }
        private void FocusOnLogin()
        {
            FocusOn(this.emailNicknameBox);
        }
        private void FocusOnPassword()
        {
            FocusOn(this.passwordBox);
        }
        private void FocusOn(Control control)
        {
            control.Focusable = true;
            control.Focus();
            control.ForceCursor = true;
            Keyboard.Focus(control);
        }
        private bool IsLoginButtonUsable()
        {
            return this.loginButton.IsEnabled;
        }
        private void ChangeLoginTheme(bool isDarkMode)
        {
            this.Resources.MergedDictionaries.Clear();
            string resourceName = isDarkMode ? "DarkModeDictionary" : "BrightModeDictionary";
            ResourceDictionary resourceDictionary = new ResourceDictionary() { Source = new Uri(string.Format("ResourceDictionaries/{0}.xaml", resourceName), UriKind.Relative) };
            this.Resources.MergedDictionaries.Add(resourceDictionary);
        }
    }
}
