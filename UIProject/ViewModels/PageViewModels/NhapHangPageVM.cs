﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIProject.ViewModels.FunctionInterfaces;

namespace UIProject.ViewModels.PageViewModels
{
    class NhapHangPageVM : BasePageViewModel 
    {
        public NhapHangPageVM() : base() { }
        public NhapHangPageVM(INavigator navigator) : base(navigator) { }
        protected override void LoadPageComponents()
        {
            
        }
    }
}
