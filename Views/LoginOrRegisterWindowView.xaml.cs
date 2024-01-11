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
            DataContext = new LoginOrRegisterWindowViewModel(CloseWindow, FocusOnLogin, FocusOnPassword, IsLoginButtonUsable, ChangeLoginTheme);
        }
        private void CloseWindow()
        {
            this.Close();
        }
        private void FocusOnLogin()
        {
            this.emailNicknameBox.Focusable = true;
            this.emailNicknameBox.Focus();
            Keyboard.Focus(this.emailNicknameBox);
        }
        private void FocusOnPassword()
        {
            this.passwordBox.Focusable = true;
            this.passwordBox.Focus();
            Keyboard.Focus(this.passwordBox);
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
