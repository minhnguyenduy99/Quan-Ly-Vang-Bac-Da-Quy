using BaseMVVM_Service.BaseMVVM;
using BaseMVVM_Service.BaseMVVM.Interfaces;
using ModelProject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIProject.Events;
using UIProject.ViewModels.FunctionInterfaces;

namespace UIProject.ViewModels.DataViewModels
{
    public class NhapHangViewModel : BaseViewModel, ISubmitViewModel
    {
        /// <summary>
        /// This property is not implemented in this class
        /// </summary>
        public ISubmitable Data { get; set; } = null;

        public bool IsDataValid { get; private set; }

        public PhieuMuaModel PhieuMua { get; private set; }
        public ObservableCollection<ChiTietMuaModel> DSChiTietMua { get; private set; }

        public event EventHandler<SubmitedDataEventArgs> SubmitedData;

        public bool Submit()
        {
            throw new NotImplementedException();
        }
    }
}
