using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIProject.ViewModels.FunctionInterfaces;

namespace UIProject.ViewModels.PageViewModels
{
    class KhachHangPageVM : BasePageViewModel
    {
        public KhachHangPageVM() : base() { }
        public KhachHangPageVM(INavigator navigator) : base(navigator) { }
        protected override void LoadPageComponents()
        {
            
        }
    }
}
