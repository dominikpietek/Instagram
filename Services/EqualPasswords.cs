using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Instagram.Services
{
    public static class EqualPasswords
    {
        public static bool Equal(string firstPassword, string secondPassword)
        {
            if (firstPassword == secondPassword)
            {
                return true;
            }
            else
            {
                MessageBox.Show(
                    "Passwords are not the same!", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
                // do not open new window !!!!!!!
            }
        }
    }
}
