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
using UIProject.ServiceProviders;

namespace UIProject.Views
{
    /// <summary>
    /// Interaction logic for ServiceAddingDialogWindow.xaml
    /// </summary>
    public partial class ServiceAddingDialogWindow : Window, IWindowExtension
    {
        public FrameworkElement Activator { get; set; }

        public ServiceAddingDialogWindow(FrameworkElement activator)
        {
            InitializeComponent();
            this.Activator = activator;
        }

        public bool? ShowDialog(Point position)
        {
            return this.ShowDialog(position);
        }

        public bool? ShowDialog(double dentaX, double dentaY)
        {
            if (Activator == null)
            {
                throw new Exception("Activator cannot be null");
            }
            return this.ShowDialog(Activator, dentaX, dentaY);
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
