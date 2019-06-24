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
using UIProject.ViewModels;
using UIProject.ServiceProviders;


namespace UIProject.Views
{
    /// <summary>
    /// Interaction logic for ProductAddingDialogWindow.xaml
    /// </summary>
    public partial class ProductAddingDialogWindow : Window, IWindowExtension
    {
        public ProductAddingDialogWindow(FrameworkElement activator)
        {
            InitializeComponent();
            Activator = activator;
            this.Loaded += ProductAddingDialogWindow_Loaded;

        }

        private async void ProductAddingDialogWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await AnimationHelper.FadeAsync(this, 0f, 1.0f);
        }

        public FrameworkElement Activator { get; set; }

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
