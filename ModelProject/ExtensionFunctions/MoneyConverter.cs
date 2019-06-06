using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject.ExtensionFunctions
{
    public static class MoneyConverter
    {
        public static string INVALID_MESSAGE_ERROR = "The argument is not in correct format or type";
        public static string NEGATIVE_MONEY_VALUE_ERROR = "The money cannot be negative";
        public static string ConvertToMoneyFormat(object value)
        {
            if (string.IsNullOrEmpty(value.ToString()))
                throw new ArgumentException(INVALID_MESSAGE_ERROR);
            decimal castMoney = Convert.ToDecimal(value.ToString());
            if (castMoney < 0)
                throw new ArgumentException(NEGATIVE_MONEY_VALUE_ERROR);
            return string.Format("{0:N0}", Convert.ToDecimal(value.ToString()));
        }
    }
}
