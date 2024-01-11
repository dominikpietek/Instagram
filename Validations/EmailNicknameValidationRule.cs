using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Instagram.Validations
{
    public class EmailNicknameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string valueString = value.ToString();
            if (valueString.Length < 3)
            {
                return new ValidationResult(false, "Too short!");
            }
            else if (valueString.Length > 15) 
            {
                return new ValidationResult(false, "Too long!");
            }
            return ValidationResult.ValidResult;
        }
    }
}
