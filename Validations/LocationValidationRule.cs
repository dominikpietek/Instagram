using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Instagram.Validations
{
    public class LocationValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string stringValue = value.ToString();
            if (stringValue.Length <= 1)
            {
                return new ValidationResult(false, "Location is too short!");
            }
            if (stringValue.Length > 20)
            {
                return new ValidationResult(false, "Location is too long!");
            }
            return ValidationResult.ValidResult;
        }
    }
}
