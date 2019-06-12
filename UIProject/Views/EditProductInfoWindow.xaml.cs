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
    /// Interaction logic for EditProductInfoWindow.xaml
    /// </summary>
    public partial class EditProductInfoWindow : Window, IWindowExtension
    {
        public EditProductInfoWindow()
        {
            InitializeComponent();
        }

        public FrameworkElement Activator { get; set; }

        public bool? ShowDialog(Point position)
        {
            return this.ShowDialog(position);
        }

        public bool? ShowDialog(double dentaX, double dentaY)
        {
            if (Activator == null)
                throw new Exception("Activator cannot be null when using these showing method");

            return this.ShowDialog(Activator, dentaX, dentaY);
        }
    }
}
