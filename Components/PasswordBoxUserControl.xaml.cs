using System;
using System.Windows;
using System.Windows.Controls;

namespace Instagram.Components
{
    public partial class PasswordBoxUserControl : UserControl
    {
        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(PasswordBoxUserControl), new PropertyMetadata(String.Empty));

        public PasswordBoxUserControl()
        {
            InitializeComponent();
        }
        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Password = PasswordBoxName.Password;
        }
    }
}
