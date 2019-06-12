using BaseMVVM_Service.BaseMVVM;
using BaseMVVM_Service.BaseMVVM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UIProject.Events;
using UIProject.UIConnector;
using UIProject.ViewModels.FunctionInterfaces;

namespace UIProject.ViewModels.LayoutViewModels
{
    public class EditWindowViewModel<T> : BaseWindowViewModel, ISubmitViewModel where T : BaseSubmitableModel
    {
        public ISubmitable Data { get; set; }

        public bool IsDataValid { get; set; } = true;

        public List<IEnumerable<BaseSubmitableModel>> AdditionData { get; private set; }
        public List<ISearcher> Searchers { get; private set; }

        public event EventHandler<SubmitedDataEventArgs> SubmitedData;

        public EditWindowViewModel(T model)
        {
            NavigationBarVisibility = System.Windows.Visibility.Collapsed;


            Data = (T)Activator.CreateInstance(typeof(T), null);
            model.Clone((T)Data);

            AdditionData = new List<IEnumerable<BaseSubmitableModel>>();
            Searchers = new List<ISearcher>();
        }

        public ICommand UpdateCommand
        {
            get => new BaseCommand<IWindowExtension>(OnUpdateCommandExecute);
        }

        public ICommand CancelCommand
        {
            get => new BaseCommand<IWindowExtension>(OnCancelCommandExecute);
        }


        public bool Submit()
        {
            if (IsDataValid == false)
                return false;
            bool submitSuccess = Data.Submit(SubmitType.Update);
            OnSubmitedData(new SubmitedDataEventArgs(Data, submitSuccess));
            return submitSuccess;
        }

        protected virtual void OnCancelCommandExecute(IWindowExtension window)
        {
            TriggerTheClosingHandler(window);
            window.DialogResult = false;
            window.Close();
        }
        protected virtual void OnUpdateCommandExecute(IWindowExtension window)
        {
            TriggerTheClosingHandler(window);
            window.DialogResult = Submit();
            window.Close();
        }

        protected virtual void OnSubmitedData(SubmitedDataEventArgs e)
        {
            SubmitedData?.Invoke(this, e);
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
