using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIProject.ViewModels.PageViewModels
{
    public class BanHangPageVM : BasePageViewModel
    {
        /// <summary>
        /// The product name that is being typed
        /// </summary>
        public string TypingProductName
        {
            get => GetPropertyValue<string>();
            set => SetProperty(value);
        }

        public string TypingCustomerName
        {
            get => GetPropertyValue<string>();
            set => SetProperty(value);
        }

        public BanHangPageVM() : base() { }

        protected override void LoadPageComponents()
        {
            TakeFullScreen = true;
        }

    }
}
