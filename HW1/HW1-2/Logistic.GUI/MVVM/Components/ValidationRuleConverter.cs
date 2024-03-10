using Logistic.GUI.MVVM.Validations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Logistic.GUI.MVVM.Components
{
    public class ValidationRuleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Type selectedValidationRuleType = value as Type;

            if (selectedValidationRuleType == typeof(NumberInputValidation))
            {
                return new NumberInputValidation();
            }
            else if (selectedValidationRuleType == typeof(StringInputValidation))
            {
                return new StringInputValidation();
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
