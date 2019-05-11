using BaseMVVM_Service.BaseMVVM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseMVVM_Service.BaseMVVM
{
    /// <summary>
    /// Base model class for model type
    /// </summary>
    public abstract class BaseModel : ObservableObject, ISubmitable
    {
        public abstract bool Submit();
    }
}
