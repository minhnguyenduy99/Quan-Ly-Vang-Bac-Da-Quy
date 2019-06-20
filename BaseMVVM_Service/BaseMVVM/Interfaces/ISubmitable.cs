using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseMVVM_Service.BaseMVVM.Interfaces
{
    /// <summary>
    /// Represents the type of data submit into database
    /// </summary>
    public enum SubmitType
    {
        Add = 0,
        Update = 1,
        Delete = 2
    }

    /// <summary>
    /// Data is provided with submit ability to be saved in Database
    /// </summary>
    /// <typeparam name="T">Data type</typeparam>
    public interface ISubmitable
    { 
        bool IsDataValid { get; }
        /// <summary>
        /// Submit data to database
        /// </summary>
        /// <param name="submitType">The type of submition</param>
        /// <returns></returns>
        bool Submit(SubmitType submitType);

        /// <summary>
        /// Event occurs when the data is submited
        /// </summary>
        event EventHandler<SubmitEventArgs> Submited;
    }

    /// <summary>
    /// Provides information about events related to data submition
    /// </summary>
    public class SubmitEventArgs: EventArgs
    {
        public bool SubmitResult { get; private set; }
        public SubmitType SubmitType { get; private set; }
        public SubmitEventArgs(bool submitResult, SubmitType submitType)
        {
            SubmitResult = submitResult;
            SubmitType = submitType;
        }
    }
}
