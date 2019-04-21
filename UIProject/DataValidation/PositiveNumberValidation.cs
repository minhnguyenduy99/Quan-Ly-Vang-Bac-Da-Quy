using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace UIProject.DataValidation
{
    public class PositiveNumberValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                int intVal = (int)value;
                if (intVal <= 0)
                    return new ValidationResult(false, "Non-positive number is illegal");
                return new ValidationResult(true, null);
            }
            catch
            {
                return new ValidationResult(false, "Illegal number");
            }
        }
    }
}
