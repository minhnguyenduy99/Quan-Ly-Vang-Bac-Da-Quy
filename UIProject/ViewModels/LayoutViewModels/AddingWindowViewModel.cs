using BaseMVVM_Service.BaseMVVM;
using ModelProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UIProject.ServiceProviders;
using UIProject.ViewModels.LayoutViewModels;

namespace UIProject.ViewModels
{
    public class AddingWindowViewModel<T> : BaseWindowViewModel<T> where T:BaseModel 
    {
        private ICommand submitCmd;
        private ICommand cancelCmd;



        public ICommand SubmitCommand
        {
            get => submitCmd ?? new BaseCommand<IClosable>(OnSubmitCommandExecute);
            set => submitCmd = value;
        }

        public ICommand CancelCommand
        {
            get => cancelCmd ?? new BaseCommand<IClosable>(OnCancelCommandExecute);
            set => cancelCmd = value;
        }

        protected virtual void OnSubmitCommandExecute(IClosable window)
        {
            OnExitWindow(window);
        }

        protected virtual void OnCancelCommandExecute(IClosable window)
        {
            OnExitWindow(window);
        }


        protected override void OnExitWindow(IClosable window)
        {
            base.OnExitWindow(window);
        }


    }
}
