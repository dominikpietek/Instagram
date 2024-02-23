using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Instagram.MessageBoxes
{
    public static class CustomMessageBox
    {
        public static void Error(string message)
        {
            MessageBox.Show(
                        message, "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
