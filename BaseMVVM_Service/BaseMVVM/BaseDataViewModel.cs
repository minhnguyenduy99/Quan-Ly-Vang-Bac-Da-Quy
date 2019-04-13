using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseMVVM_Service.BaseMVVM
{
    /// <summary>
    /// View model provides interaction between database and UI 
    /// </summary>
    public class BaseDataViewModel<T> : BaseViewModel<T> where T: BaseDataModel
    {
        public BaseDataViewModel(T model)
        {
            ModelData = model;
        }
    }
}
