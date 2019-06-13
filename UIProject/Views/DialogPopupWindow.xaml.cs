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
using UIProject.UIConnector;

namespace UIProject.Views
{
    /// <summary>
    /// Interaction logic for DialogPopupWindow.xaml
    /// </summary>
    public partial class DialogPopupWindow : Window, IWindow
    {
        public BaseWindowViewModel ViewModel { get; set; }
        public DialogPopupWindow()
        {
            InitializeComponent();

            this.DataContextChanged += DialogPopupWindow_DataContextChanged;
        }

        private void DialogPopupWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is DialogWindowViewModel)
            {
                this.Content = DataContext;
                ApplyTemplateForViewModel();
            }
        }

        private void ApplyTemplateForViewModel()
        {
            switch((DataContext as DialogWindowViewModel).DialogType)
            {
                case DialogWindowType.Waiting: this.ContentTemplate = GetDialogContentTemplate("WaitingDialogTemplate");break;
                case DialogWindowType.YesNo: this.ContentTemplate = GetDialogContentTemplate("YesNoDialogTemplate");break;
                case DialogWindowType.WaitingMessage: this.ContentTemplate = GetDialogContentTemplate("WaitingMessageDialogTemplate");break;
                case DialogWindowType.WaitingMessageCancel: this.ContentTemplate = GetDialogContentTemplate("WaitingMessageCancelTemplate");break;
                case DialogWindowType.Info: this.ContentTemplate = GetDialogContentTemplate("MessageDialogTemplate");break;
            }
        }

        private DataTemplate GetDialogContentTemplate(string resource)
        {
            return (DataTemplate)Application.Current.FindResource(resource);
        }
    }
}
