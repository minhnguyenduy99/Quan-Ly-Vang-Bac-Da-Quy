using BaseMVVM_Service.BaseMVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIProject.ServiceProviders
{
    /// <summary>
    /// Provides the view model to the UI layer. The view model has to inherit from <see cref="BaseViewModel"/>
    /// </summary>
    interface IViewModelPresenter
    {
        BaseViewModel ViewModel { get; set; }
        void ApplyViewModel(BaseViewModel viewModel);
    }
}
