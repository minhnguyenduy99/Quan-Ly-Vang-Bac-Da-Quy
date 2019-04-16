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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UIProject.CustomControls
{
    /// <summary>
    /// Interaction logic for SearchTextBox.xaml
    /// </summary>
    public partial class SearchTextBox : UserControl
    {
        public SearchTextBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The source of the icon of <see cref="SearchTextBox"/>
        /// </summary>
        public ImageSource IconSource
        {
            get { return (ImageSource)GetValue( IconSourceProperty); }
            set { SetValue( IconSourceProperty, value); }
        }

        /// <summary>
        /// The text in the <see cref="SearchTextBox"/>
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// The hint text in the <see cref="SearchTextBox"/>
        /// </summary>
        public string Hint
        {
            get { return (string)GetValue(HintProperty); }
            set { SetValue(HintProperty, value); }
        }

        /// <summary>
        /// The width of textbox control in the <see cref="SearchTextBox"/> 
        /// </summary>
        public double TextBoxWidth
        {
            get { return (double)GetValue(TextBoxWidthProperty); }
            set { SetValue(TextBoxWidthProperty, value); }
        }

        /// <summary>
        /// The height of the icon in <see cref="SearchTextBox"/>
        /// </summary>
        public double IconHeight
        {
            get { return (double)GetValue(IconHeightProperty); }
            set { SetValue(IconHeightProperty, value); }
        }

        /// <summary>
        /// The background of icon part in <see cref="SearchTextBox"/>
        /// </summary>
        public Brush IconBackground
        {
            get => (Brush)GetValue(IconBackgroundProperty);
            set => SetValue(IconBackgroundProperty, value);
        }

        #region Dependency Properties
        public static readonly DependencyProperty IconSourceProperty = DependencyProperty.Register(
            "IconSource", 
            typeof(ImageSource), 
            typeof(SearchTextBox), 
            new PropertyMetadata(null));


        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text",
            typeof(string),
            typeof(SearchTextBox),
            new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty HintProperty = DependencyProperty.Register(
            "Hint",
            typeof(string),
            typeof(SearchTextBox),
            new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty TextBoxWidthProperty = DependencyProperty.Register(
            "TextBoxWidth",
            typeof(double),
            typeof(SearchTextBox));

        public static readonly DependencyProperty IconHeightProperty = DependencyProperty.Register(
            "IconHeight",
            typeof(double),
            typeof(SearchTextBox));

        public static readonly DependencyProperty IconBackgroundProperty = DependencyProperty.Register(
            "IconBackground",
            typeof(Brush),
            typeof(SearchTextBox));

        #endregion
    }
}
