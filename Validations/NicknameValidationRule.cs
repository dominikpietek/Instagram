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
    public class NicknameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9]{3,15}$");
            string valueString = value.ToString();
            if (regex.IsMatch(valueString))
            {
                return ValidationResult.ValidResult;
            }
            else if (valueString.Length < 3)
            {
                return new ValidationResult(false, "Nickname is too short!");
            }
            else if (valueString.Length > 15)
            {
                return new ValidationResult(false, "Nickname is too long!");
            }
            else
            {
                return new ValidationResult(false, "Nickname can't contain special characters!");
            }
        }
    }
}
