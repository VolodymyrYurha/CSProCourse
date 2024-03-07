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
    public class FormValidator
    {

        public ValidationResult ValidateVehicleNumber(string input)
        {
            Regex regex = new Regex(@"^[A-Z]{2}\s?\d{4}\s?[A-Z]{2}$");

            if (!regex.IsMatch(input))
            {
                return new ValidationResult(false, "Incorrect form. Expected: AA 1111 AA");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
        public ValidationResult ValidateWeightInt(string input)
        {
            Regex regex = new Regex(@"^\d+$");

            if (!regex.IsMatch(input))
            {
                return new ValidationResult(false, "Incorrect form. Expected: 123");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }

        public ValidationResult ValidateVolumeFloat(string input)
        {
            Regex regex = new Regex(@"^[0-9]*\.?[0-9]+$");
            //Regex regex = new Regex(@"^[0-9]*,?[0-9]+$");
            //Regex regex = new Regex(@"^[0-9]*[,\.]?[0-9]+$");


            if (!regex.IsMatch(input))
            {
                return new ValidationResult(false, "Incorrect format. Expected: 12.3");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }

    }
}
