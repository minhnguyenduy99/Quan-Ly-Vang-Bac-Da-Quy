using UIProject.ServiceProviders;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UIProject.ViewModels.LayoutViewModels;
using BaseMVVM_Service.BaseMVVM;

namespace UIProject.Views
{
    /// <summary>
    /// Interaction logic for DialogPopupWindow.xaml
    /// </summary>
    public partial class DialogPopupWindow : Window, IWindow, IViewModelPresenter
    {
        public BaseViewModel ViewModel { get; set; }
        public DialogPopupWindow()
        {
            InitializeComponent(); 
        }

        public DialogPopupWindow(DialogWindowViewModel dialogWindowVM) : this()
        {
            ApplyViewModel(dialogWindowVM);
            ApplyTemplateForViewModel();
        }

        private void ApplyTemplateForViewModel()
        {
            switch((ViewModel as DialogWindowViewModel).DialogType)
            {
                case DialogWindowType.Waiting: this.ContentTemplate = GetDialogContentTemplate("WaitingDialogTemplate");break;
                case DialogWindowType.YesNo: this.ContentTemplate = GetDialogContentTemplate("YesNoDialogTemplate");break;
                case DialogWindowType.WaitingMessage: this.ContentTemplate = GetDialogContentTemplate("WaitingMessageDialogTemplate");break;
                case DialogWindowType.WaitingMessageCancel: this.ContentTemplate = GetDialogContentTemplate("WaitingMessageCancelTemplate");break;
            }
        }


        public void ApplyCustomTemplateForViewModel(string resource)
        {
            this.ContentTemplate = GetDialogContentTemplate(resource);
        }

        private DataTemplate GetDialogContentTemplate(string resource)
        {
            return (DataTemplate)Application.Current.FindResource(resource);
        }

        
        public void ApplyViewModel(BaseViewModel viewModel)
        {
            this.ViewModel = viewModel;
            this.DataContext = viewModel;
            this.Content = viewModel;
        }      
    }
}
