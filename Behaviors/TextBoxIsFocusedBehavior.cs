using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Instagram.Behaviors
{
    public static class TextBoxIsFocusedBehavior
    {
        public static readonly DependencyProperty IsFocusedCommandProperty =
            DependencyProperty.RegisterAttached(
                "IsFocusedCommand", typeof(System.Windows.Input.ICommand), typeof(TextBoxIsFocusedBehavior),
                new FrameworkPropertyMetadata(null, OnIsFocusedCommandChanged));

        public static System.Windows.Input.ICommand GetIsFocusedCommand(DependencyObject obj)
        {
            return (System.Windows.Input.ICommand)obj.GetValue(IsFocusedCommandProperty);
        }

        public static void SetIsFocusedCommand(DependencyObject obj, System.Windows.Input.ICommand value)
        {
            obj.SetValue(IsFocusedCommandProperty, value);
        }

        private static void OnIsFocusedCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox)
            {
                if (e.NewValue is System.Windows.Input.ICommand command)
                {
                    textBox.GotFocus += (sender, args) =>
                    {
                        if (command.CanExecute(null))
                        {
                            command.Execute(true);
                        }
                    };

                    textBox.LostFocus += (sender, args) =>
                    {
                        if (command.CanExecute(null))
                        {
                            command.Execute(false);
                        }
                    };
                }
            }
        }
    }
}
