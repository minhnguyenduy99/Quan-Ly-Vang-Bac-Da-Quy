using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace UIProject.Behaviors
{
    public class DataGridSetColumnStyle
    {
        public static Style GetElementStyle(DataGrid obj)
        {
            return (Style)obj.GetValue(ElementStyleProperty);
        }


        public static void SetElementstyle(DataGrid obj, Style value)
        {
            obj.SetValue(ElementStyleProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ElementStyleProperty =
            DependencyProperty.RegisterAttached(
                "ElementStyle",
                typeof(Style),
                typeof(DataGridSetColumnStyle),
                new PropertyMetadata(null, OnSetElementStyle));

        private static void OnSetElementStyle(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dataGrid = d as DataGrid;
            if (dataGrid == null)
                return;
            if (dataGrid.Columns.Count == 0)
            {
                dataGrid.Loaded += (sender, emptyEvent) =>
                {
                    ApplyElementStyleToDataGrid(dataGrid, e.NewValue);
                };
            }
            else
            {
                ApplyElementStyleToDataGrid(dataGrid, e.NewValue);
            }
        }

        private static void ApplyElementStyleToDataGrid(DataGrid dataGrid, object value)
        {
            Style elementStyle = value as Style;
            if (elementStyle != null)
            {
                var textColumns = dataGrid.Columns.Where(column => column is DataGridTextColumn).Cast<DataGridTextColumn>();
                foreach (var column in textColumns)
                {
                    column.ElementStyle = elementStyle;
                }
            }
        }
    }
}
