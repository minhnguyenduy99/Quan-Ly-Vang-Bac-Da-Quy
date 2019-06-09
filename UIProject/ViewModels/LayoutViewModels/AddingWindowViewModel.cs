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
        public ISubmitable Data { get; set; }
        public bool IsDataValid { get; private set; } 
        public List<IEnumerable<BaseSubmitableModel>> AdditionData { get; private set; }
        public List<ISearcher> Searchers { get; private set; }

        public ICommand SubmitCommand
        {
            get => submitCmd ?? new BaseCommand<IWindowExtension>(OnSubmitCommandExecute);
            set => submitCmd = value;
        }



        public AddingWindowViewModel() : base()
        {
            this.NavigationBarVisibility = System.Windows.Visibility.Collapsed;

            // Automatically create an instance of data
            Data = (ISubmitable)Activator.CreateInstance(typeof(T), null);

            AdditionData = new List<IEnumerable<BaseSubmitableModel>>();
            Searchers = new List<ISearcher>();

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

        protected virtual void OnSubmitCommandExecute(IWindowExtension window)
        {
            Submit();
        }

    }
}
