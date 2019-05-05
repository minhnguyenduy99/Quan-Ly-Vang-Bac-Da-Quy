using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseMVVM_Service.BaseMVVM.Interfaces
{
    /// <summary>
    /// Data is provided with submit ability to be saved in Database
    /// </summary>
    /// <typeparam name="T">Data type</typeparam>
    public interface ISubmitable
    { 
        /// <summary>
        /// Submit data to database
        /// </summary>
        /// <returns></returns>
        bool Submit();
    }
}
