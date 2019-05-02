using BaseMVVM_Service.BaseMVVM.Interfaces;
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
    public abstract class BaseViewModel : ObservableObject
    { 
    }

    public abstract class BaseViewModel<T> : ObservableObject
    {
        T model;
        public T Model
        {
            get => model;
            set => SetProperty(ref model, value);
        }
    }
}
