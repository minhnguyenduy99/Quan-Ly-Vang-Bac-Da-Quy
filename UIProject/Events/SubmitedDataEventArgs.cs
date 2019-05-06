using BaseMVVM_Service.BaseMVVM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIProject.Events
{
    /// <summary>
    /// Provides information about <see cref="SubmitedData"/> event
     /// </summary>
    public class SubmitedDataEventArgs : EventArgs
    {
        /// <summary>
        /// The data assumed to be submited
        /// </summary>      
        public ISubmitable Data { get; private set; }

        /// <summary>
        /// The result of submition
        /// </summary>
        public bool Result { get; private set; }

        public SubmitedDataEventArgs(ISubmitable data, bool result)
        {
            Data = data;
            Result = result;
        }
    }
}
