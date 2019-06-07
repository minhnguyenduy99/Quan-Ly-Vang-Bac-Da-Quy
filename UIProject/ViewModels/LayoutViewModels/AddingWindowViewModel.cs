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
using UIProject.UIConnector;
using UIProject.ViewModels.FunctionInterfaces;
using UIProject.ViewModels.LayoutViewModels;


namespace UIProject.ViewModels
{
    public class AddingWindowViewModel<T> : BaseWindowViewModel, ISubmitViewModel where T:BaseSubmitableModel 
    {
        private ICommand submitCmd;
        private ICommand cancelCmd;

        public ISubmitable Data { get; set; }

        public bool IsDataValid { get; private set; } 

        public List<IEnumerable<BaseSubmitableModel>> AdditionData { get; set; }

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

            // Automatically create an instance of data
            Data = (ISubmitable)Activator.CreateInstance(typeof(T), null);
            AdditionData = new List<IEnumerable<BaseSubmitableModel>>();
            IsDataValid = true;
        }

        public event EventHandler<SubmitedDataEventArgs> SubmitedData;

        /// <summary>
        /// Submit data to database
        /// </summary>
        /// <returns></returns>
        public bool Submit()
        {
            bool submitSuccess = Data.Submit(SubmitType.Add);
            IsDataValid = submitSuccess;
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
