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
        protected override void LoadPageComponents()
        {
            this.TakeFullScreen = true;       
        }

        public LamDichVuPageVM() : base() { }
        public LamDichVuPageVM(INavigator navigator) : base(navigator) { }
    }
}
