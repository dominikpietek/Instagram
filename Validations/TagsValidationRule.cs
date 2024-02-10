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
    public class TagsValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string stringValue = value.ToString();
            var regex = new Regex(@"^[a-zA-Z\s]{1,100}$");
            if (regex.IsMatch(stringValue))
            {
                return ValidationResult.ValidResult;
            }
            if (stringValue.Length > 100)
            {
                return new ValidationResult(false, "Tags are too long!");
            }
            return new ValidationResult(false, "Tags can contain only letters ans spaces!");
        }
    }
}
