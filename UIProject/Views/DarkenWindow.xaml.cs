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

namespace UIProject.Views
{
    /// <summary>
    /// Interaction logic for DarkenWindow.xaml
    /// </summary>
    public partial class DarkenWindow : Window
    {
        public DarkenWindow()
        {
            InitializeComponent();           
        }

        public DarkenWindow(Page page) : this()
        {
            PART_PAGE.Content = page;
        }
    }
}
