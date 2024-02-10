using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Instagram.Validations
{
    public class EmailValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex regex = new Regex(@"[a-z0-9\.?]+@[a-z]+\.+[a-z]{2,3}");
            string stringValue = value.ToString();
            if (!stringValue.Contains('@'))
            {
                return new ValidationResult(false, "email has to contain '@'");
            }
            string userIdentificator = stringValue.Substring(0, stringValue.IndexOf('@'));
            string afterAt = stringValue.Substring(stringValue.IndexOf('@') + 1);
            Regex userIdentificatorRegex = new Regex(@"^[a-z0-9\.]{1,100}$");
            Regex afterAtRegex = new Regex(@"^[a-z]{1,10}\.+[a-z]+[a-z]$");
            if ((userIdentificator[0] == '.') || (userIdentificator[userIdentificator.Length - 1] == '.'))
            {
                return new ValidationResult(false, "user identificator can't have '.' on start and end");
            }
            if (!userIdentificatorRegex.IsMatch(userIdentificator))
            {
                return new ValidationResult(false, "user identificator can only contain letters, numbers and dots");
            }
            if (afterAt.Count(s => s == '.') != 1)
            {
                return new ValidationResult(false, "after '@' has to be only one '.'");
            }
            if (!afterAtRegex.IsMatch(afterAt))
            {
                return new ValidationResult(false, "after '@' has to be only letters (and one dot)");
            }
            string afterLastDot = afterAt.Substring(afterAt.IndexOf('.') + 1);
            if (!(afterLastDot.Length == 2 || afterLastDot.Length == 3))
            {
                return new ValidationResult(false, "after last '.' can be only 2 to 3 letters");
            }
            if (regex.IsMatch(stringValue))
            {
                return ValidationResult.ValidResult;
            }
            return new ValidationResult(false, "wrong email");
        }
    }
}
