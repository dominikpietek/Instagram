using Instagram.Databases;
using Instagram.JSONModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Instagram.Components
{
    /// <summary>
    /// Interaction logic for PasswordBoxUserControl.xaml
    /// </summary>
    public partial class PasswordBoxUserControl : UserControl
    {
        public string BackgroundColour
        {
            get { return (string)GetValue(BackgroundColourProperty); }
            set { SetValue(BackgroundColourProperty, value); }
        }
        public static readonly DependencyProperty BackgroundColourProperty =
            DependencyProperty.Register("BackgroundColour", typeof(string), typeof(PasswordBoxUserControl), new PropertyMetadata(String.Empty));
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
