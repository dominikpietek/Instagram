using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace Instagram.Behaviors
{
    public static class TextBoxTextChangedBehavior
    {
        public static readonly DependencyProperty TextChangedCommandProperty =
            DependencyProperty.RegisterAttached("TextChangedCommand", typeof(ICommand),
                typeof(TextBoxTextChangedBehavior), new FrameworkPropertyMetadata(null, OnTextChangedCommandChanged));

        public static ICommand GetTextChangedCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(TextChangedCommandProperty);
        }

        public static void SetTextChangedCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(TextChangedCommandProperty, value);
        }

        private static void OnTextChangedCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox)
            {
                textBox.TextChanged -= TextBox_TextChanged;
                if (e.NewValue != null)
                {
                    textBox.TextChanged += TextBox_TextChanged;
                }
            }
        }

        private static void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                ICommand command = GetTextChangedCommand(textBox);
                if (command != null && command.CanExecute(textBox.Text))
                {
                    command.Execute(textBox.Text);
                }
            }
        }
    }
}
