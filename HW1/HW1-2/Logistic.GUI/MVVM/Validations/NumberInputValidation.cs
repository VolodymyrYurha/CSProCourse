using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Logistic.GUI.MVVM.Validations
{
    class NumberInputValidation: ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex regex = new Regex(@"^[A-Z]{2}\s?\d{4}\s?[A-Z]{2}$");

            if (!regex.IsMatch(value.ToString()))
            {
                return new ValidationResult(false, "Enter correctly");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }
}
