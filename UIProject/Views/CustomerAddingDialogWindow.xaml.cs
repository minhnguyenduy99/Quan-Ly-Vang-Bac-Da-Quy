using ModelProject;
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
using UIProject.Events;
using UIProject.ServiceProviders;
using UIProject.ViewModels;
using UIProject.ViewModels.LayoutViewModels;

namespace UIProject.Views
{
    /// <summary>
    /// Interaction logic for CustomerAddingDialogWindow.xaml
    /// </summary>
    public partial class CustomerAddingDialogWindow : Window, IWindow
    {
        public AddingWindowViewModel<KhachHangModel> ViewModel { get; set; }
        public CustomerAddingDialogWindow()
        {
            InitializeComponent();
            ViewModel = new AddingWindowViewModel<KhachHangModel>();

            DataContext = ViewModel;

            PART_Combobox.ItemsSource = DataAccess.LoadKhuVuc();
        }
    }
}
