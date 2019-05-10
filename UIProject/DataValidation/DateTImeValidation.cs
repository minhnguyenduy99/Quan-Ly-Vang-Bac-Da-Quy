using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace UIProject.DataValidation
{
    public class DateTImeValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                DateTime castDate = DateTime.ParseExact(value.ToString(), "dd/MM/yyyy", null);
            }
            catch { return new ValidationResult(false, "Thời gian không chính xác"); }
            return new ValidationResult(true, string.Empty);
        }
    }
}
