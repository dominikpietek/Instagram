using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Instagram.Validations
{
    public class EmailValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex regex = new Regex(@"[a-z0-9]+\.?[a-z0-9]+@[a-z]+\.[a-z]{2,3}");
            string stringValue = value.ToString();
            if (regex.IsMatch(stringValue))
            {
                return ValidationResult.ValidResult;
            }
            else
            {   
                if (!stringValue.Contains('@'))
                {
                    return new ValidationResult(false, "email has to contain '@'");
                }
                else
                {
                    string userIdentificator = stringValue.Substring(0, stringValue.IndexOf('@') + 1);
                    string afterAt = stringValue.Substring(stringValue.IndexOf('@') + 1);
                    Regex userIdentificatorRegex = new Regex("[a-z0-9.]");
                    Regex afterAtRegex = new Regex(@"[a-z]+\.+[a-z]");
                    if (userIdentificator[0] == '.' || userIdentificator[userIdentificator.Length - 1] == '.')
                    {
                        return new ValidationResult(false, "user identificator can't have '.' on start and end");
                    }
                    else if (!userIdentificatorRegex.IsMatch(userIdentificator))
                    {
                        return new ValidationResult(false, "user identificator can only contain letters, numbers and dots");
                    }
                    else if (!afterAtRegex.IsMatch(afterAt))
                    {
                        return new ValidationResult(false, "after '@' has to be only letters (and one dot)"); // here is sth wrong
                    }
                    else if (afterAt.Count(s => s == '.') != 1)
                    {
                        return new ValidationResult(false, "after '@' has to be only one '.'");
                    }
                    else
                    {
                        string afterLastDot = afterAt.Substring(afterAt.IndexOf('.') + 1);
                        Regex afterLastDotRegex = new Regex("[a-z]{2,3}");
                        if (!afterLastDotRegex.IsMatch(afterLastDot))
                        {
                            return new ValidationResult(false, "after last '.' can be only 2 to 3 letters");
                        }
                    }
                    return new ValidationResult(false, "wrong email");
                }   
            }
        }
    }
}
