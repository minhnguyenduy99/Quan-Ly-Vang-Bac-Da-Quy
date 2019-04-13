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
using UIProject.ServiceProviders;

namespace UIProject.Views
{
    /// <summary>
    /// Interaction logic for DialogPopupWindow.xaml
    /// </summary>
    public partial class DialogPopupWindow : Window, IClosable
    {
        public DialogPopupWindow()
        {
            InitializeComponent();

        }
    }
}
