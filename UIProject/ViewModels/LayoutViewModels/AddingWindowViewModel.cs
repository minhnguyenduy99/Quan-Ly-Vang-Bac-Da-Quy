using BaseMVVM_Service.BaseMVVM;
using BaseMVVM_Service.BaseMVVM.Interfaces;
using ModelProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UIProject.Events;
using UIProject.ServiceProviders;
using UIProject.ViewModels.FunctionInterfaces;
using UIProject.ViewModels.LayoutViewModels;

namespace UIProject.ViewModels
{
    public class AddingWindowViewModel<T> : BaseWindowViewModel, ISubmitViewModel where T:BaseModel 
    {
        private ICommand submitCmd;
        private ICommand cancelCmd;

        public ISubmitable Data { get; set; }

        public ICommand SubmitCommand
        {
            get => submitCmd ?? new BaseCommand<IWindow>(OnSubmitCommandExecute);
            set => submitCmd = value;
        }

        public ICommand CancelCommand
        {
            get => cancelCmd ?? new BaseCommand<IWindow>(OnCancelCommandExecute);
            set => cancelCmd = value;
        }

        public AddingWindowViewModel() : base()
        {
            this.NavigationBarVisibility = System.Windows.Visibility.Collapsed;
        }

        public event EventHandler<SubmitedDataEventArgs> SubmitedData;

        public bool Submit()
        {
            bool submitSuccess = Data.Submit();
            OnSubmitedData(new SubmitedDataEventArgs(Data, submitSuccess));
            return submitSuccess;
        }

        protected virtual void OnSubmitedData(SubmitedDataEventArgs e)
        {
            SubmitedData?.Invoke(this, e);
        }

        protected virtual void OnSubmitCommandExecute(IWindow window)
        {
            Submit();
            OnExitWindow(window);
        }

        protected virtual void OnCancelCommandExecute(IWindow window)
        {
            OnExitWindow(window);
        }

        protected override void OnShowWindow(IWindow window)
        {
           
        }

        protected override void OnExitWindow(IWindow window)
        {
            base.OnExitWindow(window);
        }
    }
}
