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
            OnExitWindow(window);
        }

        protected virtual void OnUpdateCommandExecute(IWindowExtension window)
        {
            Submit();
            OnExitWindow(window);
        }

        protected virtual void OnSubmitedData(SubmitedDataEventArgs e)
        {
            SubmitedData?.Invoke(this, e);
        }
    }
}
