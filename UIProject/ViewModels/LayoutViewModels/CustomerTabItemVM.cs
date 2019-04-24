using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIProject.ViewModels.LayoutViewModels
{
    public class CustomerTabItemVM : BaseTabItemVM
    {
        private string typingCustomerName;

        /// <summary>
        /// The name of customer that is begin typed
        /// </summary>
        public string TypingCustomerName
        {
            get => typingCustomerName;
            set
            {
                string oldValue = typingCustomerName;
                SetProperty(ref typingCustomerName, value);
                OnCustomerNameChanged(oldValue, typingCustomerName);
            }
        }

        /// <summary>
        /// The phone number of customer
        /// </summary>
        public string PhoneNumber
        {
            get => GetPropertyValue<string>();
            set => SetProperty(value);
        }

        /// <summary>
        /// The address of customer
        /// </summary>
        public string Address
        {
            get => GetPropertyValue<string>();
            set => SetProperty(value);
        }

        /// <summary>
        /// The newest debt of customer
        /// </summary>
        public long Debt
        {
            get => GetPropertyValue<int>();
            set => SetProperty(value);
        }


        #region Constructor
        public CustomerTabItemVM() : base()
        {
        }
        #endregion



        protected virtual void OnCustomerNameChanged(string oldValue, string newValue)
        {
            CustomerNameChanged?.Invoke(this, new TextChangedEventArgs(oldValue, newValue));
        }

        /// <summary>
        /// Event occurs when the name of customer changed
        /// </summary>
        public event EventHandler<TextChangedEventArgs> CustomerNameChanged;
    }


    /// <summary>
    /// Provides the information about <see cref="CustomerTabItemVM{T}.CustomerNameChanged"/> event 
    /// </summary>
    public class TextChangedEventArgs: EventArgs
    {
        /// <summary>
        /// The value of text before changed
        /// </summary>
        public string OldValue { get; private set; }

        /// <summary>
        /// The new value of text after changed
        /// </summary>
        public string NewValue { get; private set; }
        
        /// <summary>
        /// Represents an instance of <see cref="TextChangedEventArgs"/>
        /// </summary>
        /// <param name="oldValue">The old value</param>
        /// <param name="newValue">The new value</param>
        public TextChangedEventArgs(string oldValue, string newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}
