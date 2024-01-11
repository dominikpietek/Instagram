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
    public class PasswordValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string stringValue = value.ToString();
            if (stringValue.Length < 8)
            {
                return new ValidationResult(false, "Password has to contain minimum 8 characters!");
            }
            else if (!stringValue.Any(s => char.IsDigit(s))){
                return new ValidationResult(false, "Password has to contain digit!");
            }
            else if (!stringValue.Any(s => char.IsLetter(s)))
            {
                return new ValidationResult(false, "Password has to contain letter!");
            }
            else if (!stringValue.Any(s => char.IsUpper(s)))
            {
                return new ValidationResult(false, "Password has to contain capital letter!");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }
}
