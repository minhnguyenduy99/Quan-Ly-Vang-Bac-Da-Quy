using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseMVVM_Service.BaseMVVM.Interfaces;

namespace UIProject.ServiceProviders
{
    class AsynchorousPerformHelper
    {
        public static async Task PerformAsync(IAsynchronousable asyncMethod, Action asyncAction)
        {
            asyncAction();
            asyncMethod.ActionAsync.Start();
            await asyncMethod.ActionAsync;
        }
    }
}
