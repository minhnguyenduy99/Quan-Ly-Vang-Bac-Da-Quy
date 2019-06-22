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

namespace UIProject.Views
{
    /// <summary>
    /// Interaction logic for ServiceListDialogWindow.xaml
    /// </summary>
    public partial class ServiceListDialogWindow : Window, IWindow
    {
        public ServiceListDialogWindow()
        {
            InitializeComponent();
            this.DataContextChanged += ServiceListDialogWindow_DataContextChanged;
        }

        private void ServiceListDialogWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            grServiceDisplayer.Content = e.NewValue;
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
