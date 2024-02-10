using Instagram.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public LoginOrRegisterWindowView()
        {
            InitializeComponent();
            DataContext = new LoginOrRegisterWindowViewModel(CloseWindow, FocusOnLogin, FocusOnPassword, IsLoginButtonUsable, ChangeLoginTheme, WhichOneIsFocused);
        }
        public void CloseWindow()
        {
            this.Close();
        }
        public int WhichOneIsFocused()
        {
            if (!this.emailNicknameBox.IsKeyboardFocusWithin && !this.passwordBox.IsKeyboardFocusWithin)
            {
                return 0;
            }
            return this.emailNicknameBox.IsKeyboardFocusWithin ? 1 : 2;
        }
        public void FocusOnLogin()
        {
            this.emailNicknameBox.Focusable = true;
            this.emailNicknameBox.Focus();
            this.emailNicknameBox.ForceCursor = true;
            Keyboard.Focus(this.emailNicknameBox);
        }
        public void FocusOnPassword()
        {
            this.passwordBox.Focusable = true;
            this.passwordBox.Focus();
            this.passwordBox.ForceCursor = true;
            Keyboard.Focus(this.passwordBox);
        }
        public bool IsLoginButtonUsable()
        {
            return this.loginButton.IsEnabled;
        }
        public void ChangeLoginTheme(bool isDarkMode)
        {
            this.Resources.MergedDictionaries.Clear();
            string resourceName = isDarkMode ? "DarkModeDictionary" : "BrightModeDictionary";
            ResourceDictionary resourceDictionary = new ResourceDictionary() { Source = new Uri(string.Format("ResourceDictionaries/{0}.xaml", resourceName), UriKind.Relative) };
            this.Resources.MergedDictionaries.Add(resourceDictionary);
        }
    }
}
