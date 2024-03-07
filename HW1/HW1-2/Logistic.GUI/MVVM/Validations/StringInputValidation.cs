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
    class StringInputValidation: ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex regex = new Regex("^[A_Za-z0-9]{2,10}$");

            if(!regex.IsMatch(value.ToString()))
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
