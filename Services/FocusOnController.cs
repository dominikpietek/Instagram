using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Instagram.Services
{
    public class FocusOnController
    {
        private readonly Control _emailControl;
        private readonly Control _passwordControl;

        public FocusOnController(Control emailControl, Control passwordControl)
        {
            _emailControl = emailControl;
            _passwordControl = passwordControl;
        }

        private void Focus(Control control)
        {
            control.Focusable = true;
            control.Focus();
            control.ForceCursor = true;
            Keyboard.Focus(control);
        }

        private int WhichOne()
        {
            if (_emailControl.IsKeyboardFocusWithin)
            {
                return 1;
            }
            else if (_passwordControl.IsKeyboardFocusWithin)
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }

        public bool WantToLoginOrChangeBoxIndex(Func<bool> isLoginButtonUsable)
        {
            int whichOneIsFocused = WhichOne();
            if (whichOneIsFocused == 0)
            {
                Focus(_emailControl);
            }
            else if (whichOneIsFocused == 1)
            {
                Focus(_passwordControl);
            }
            else if (whichOneIsFocused == 2)
            {
                if (isLoginButtonUsable.Invoke())
                {
                    return false;
                }
                else
                {
                    Focus(_emailControl);
                }
            }
            return true;
        }
    }
}
