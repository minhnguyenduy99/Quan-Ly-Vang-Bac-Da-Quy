using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIProject.ViewModels.FunctionInterfaces;

namespace UIProject.ViewModels.PageViewModels
{
    public class BaoCaoTonKhoPageVM : BasePageViewModel
    {
        public BaoCaoTonKhoPageVM(): base() { }
        public BaoCaoTonKhoPageVM(INavigator navigator) : base(navigator) { }



        protected override void LoadComponentsInternal()
        {
            throw new NotImplementedException();
        }

        protected override void ReloadComponentsInternal()
        {
            throw new NotImplementedException();
        }
    }
}
