using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIProject.ViewModels.FunctionInterfaces;

namespace UIProject.ViewModels.PageViewModels
{
    public class LamDichVuPageVM : BasePageViewModel
    {



        public LamDichVuPageVM() : base() { }
        public LamDichVuPageVM(INavigator navigator) : base(navigator) { }
        protected override void LoadComponentsInternal()
        {
            this.TakeFullScreen = true;
        }

        protected override void ReloadComponentsInternal()
        {
            throw new NotImplementedException();
        }
    }
}
