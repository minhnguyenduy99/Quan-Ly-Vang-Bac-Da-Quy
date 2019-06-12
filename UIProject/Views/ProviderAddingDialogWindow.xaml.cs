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
using UIProject.ServiceProviders;
using UIProject.UIConnector;

namespace UIProject.Views
{
    /// <summary>
    /// Interaction logic for ProviderAddingDialogWindow.xaml
    /// </summary>
    public partial class ProviderAddingDialogWindow : Window, IWindowExtension
    {
        public FrameworkElement Activator { get; set; }
        public ProviderAddingDialogWindow()
        {
            InitializeComponent();
        }

        public bool? ShowDialog(Point position)
        {
            return this.ShowDialog(position);
        }

        public bool? ShowDialog(double dentaX, double dentaY)
        {
            if (Activator == null)
                throw new Exception("The activator cannot be null");
            return this.ShowDialog(Activator, dentaX, dentaY);
        }
    }
}
