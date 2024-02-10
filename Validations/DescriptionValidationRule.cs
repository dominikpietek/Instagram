using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Instagram.Validations
{
    public class DescriptionValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string valueString = value.ToString();
            if(valueString.Length > 5)
            {
                if (valueString.Length > 100)
                {
                    return new ValidationResult(false, "Description is too long! (max 100 characters)");
                }
                return ValidationResult.ValidResult;
            }
            else
            {
                return new ValidationResult(false, "Description is too short! (min 5 characters)");
            }
        }
    }
}
