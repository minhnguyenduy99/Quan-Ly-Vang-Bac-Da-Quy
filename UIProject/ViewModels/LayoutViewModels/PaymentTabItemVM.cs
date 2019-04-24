using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIProject.ViewModels.LayoutViewModels
{
    /// <summary>
    /// Indicating the discount selection to perform 
    /// </summary>
    public enum DiscountCalculateType
    {
        /// <summary>
        /// Discount unit is in concurrency type
        /// </summary>
        Concurrency = 0,

        /// <summary>
        /// Discount unit is in percentage type
        /// </summary>
        Percentage = 1,
    }

    /// <summary>
    /// The view model of Payment tab item 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PaymentTabItemVM : TabViewModel
    {
        public readonly double DEFAULT_VAT = 0.1;

        

        /// <summary>
        /// Represents the total money
        /// </summary>
        public long TotalMoney
        {
            get => GetPropertyValue<long>();
            set => SetProperty(value);
        }

        /// <summary>
        /// Represents the VAT
        /// </summary>
        public double VAT
        {
            get => GetPropertyValue<double>();
            set => SetProperty(value);
        }


        public string Error => null;

        public string this[string columnName] => throw new NotImplementedException();
    }
}
