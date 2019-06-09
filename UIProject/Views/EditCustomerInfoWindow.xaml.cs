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
using UIProject.UIConnector;
using UIProject.ViewModels.FunctionInterfaces;
using UIProject.ViewModels.LayoutViewModels;
using UIProject.ServiceProviders;

namespace UIProject.Views
{
    /// <summary>
    /// Interaction logic for EditCustomerInfoWindow.xaml
    /// </summary>
    public partial class EditCustomerInfoWindow : Window, IWindowExtension
    {
        public FrameworkElement Activator { get; set; }

        public EditCustomerInfoWindow(ISubmitViewModel viewModel, FrameworkElement activator)
        {
            InitializeComponent();

            DataContext = viewModel;
            Activator = activator;
        }


        private void UpdateInfoHandler(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelUpdateHandler(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }


        public bool? ShowDialog(Point position)
        {
            return this.ShowDialog(position);
        }


        public bool? ShowDialog(double dentaX, double dentaY)
        {
            if (Activator == null)
                throw new Exception("Activator cannot be null");
            return this.ShowDialog(Activator, dentaX, dentaY);
        }
    }
}
