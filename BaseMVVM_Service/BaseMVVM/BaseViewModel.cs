using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseMVVM_Service.BaseMVVM
{
    /// <summary>
    /// Provides a base view model for all View Model class with a specified Data Model
    /// </summary>
    public abstract class BaseViewModel<Model>: ObservableObject
    {
        private Model modelData;

        public Model ModelData
        {
            get => this.modelData;
            set => SetProperty(ref modelData, value);
        }
    }

    /// <summary>
    /// Provides a base view model for all View Model class without a specified Data Model
    /// </summary>
    public abstract class BaseViewModel : ObservableObject
    {

    }
}
