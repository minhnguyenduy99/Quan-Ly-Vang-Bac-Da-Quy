using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIProject.ViewModels.PageViewModels
{
    public class LamDichVuPageVM : BasePageViewModel
    {
        protected override void LoadPageComponents()
        {
            this.TakeFullScreen = true;       
        }
    }
}
