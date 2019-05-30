using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIProject.ViewModels.FunctionInterfaces;

namespace UIProject.ViewModels.PageViewModels
{
    public class DichVuPageVM : BasePageViewModel
    {
        public DichVuPageVM() : base() { }
        public DichVuPageVM(INavigator navigator) : base(navigator) { }
        protected override void LoadPageComponents()
        {
            
        }
    }
}
