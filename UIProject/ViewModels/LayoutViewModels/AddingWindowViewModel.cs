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


        /// <summary>
        /// Represents the data used to submit
        /// </summary>
        public ISubmitable Data { get; set; }

        /// <summary>
        /// Indicating weither the data is already valid 
        /// </summary>
        public bool IsDataValid { get; private set; } 

        /// <summary>
        /// The additional data needed for submit fulfillment
        /// </summary>
        public List<IEnumerable<BaseSubmitableModel>> AdditionData { get; private set; }

        /// <summary>
        /// The searchers needed for submit fulfillment
        /// </summary>
        public List<ISearcher> Searchers { get; private set; }

        /// <summary>
        /// Command executes to submit the data to database
        /// </summary>
        public ICommand SubmitCommand
        {
            get => submitCmd ?? new BaseCommand<IWindowExtension>(OnSubmitCommandExecute);
            set => submitCmd = value;
        }

        /// <summary>
        /// The command executes when the submit process is canceled
        /// </summary>
        public ICommand CancelCommand
        {
            get => cancelCmd ?? new BaseCommand<IWindowExtension>(OnCancelCommandExecute);
            set => cancelCmd = value;
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

        /// <summary>
        /// Event occurs when the data is already submited
        /// </summary>
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
            TriggerTheClosingHandler(window);
            window.DialogResult = Submit();                   
            window.Close();
        }

        protected virtual void OnCancelCommandExecute(IWindowExtension window)
        {
            TriggerTheClosingHandler(window);
            window.DialogResult = false;
            window.Close();
        }

        /// <summary>
        /// When the <see cref="IWindow.ShowDialog"/> is called, there's no way to set the <see cref="IWindow.DialogResult"/>.
        /// To solve this, first trigger the closing handler to cancel it to do our task, then trigger the closing handler
        /// back to default.
        /// </summary>
        /// <param name="window"></param>
        private void TriggerTheClosingHandler(IWindowExtension window)
        {
            // Remove the cancel handler (if it does exist) 
            window.Closing -= (sender, e) => e.Cancel = true;

            // Reset the default closing-handler of the window
            window.Closing += (sender, e) => e.Cancel = false;
        }

        protected override void LoadComponentsInternal()
        {
            
        }

        protected override void ReloadComponentsInternal()
        {
        }
    }
}
