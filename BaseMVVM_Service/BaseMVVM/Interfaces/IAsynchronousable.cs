using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseMVVM_Service.BaseMVVM.Interfaces
{
    /// <summary>
    /// Provides the ability to perform methods asynchronously
    /// </summary>
    public interface IAsynchronousable
    {
        Task ActionAsync { get; set; }
    }
}
