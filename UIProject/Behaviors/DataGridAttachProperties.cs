using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using UIProject.ServiceProviders;

namespace UIProject.Behaviors
{
    public class DataGridAttachProperties
    {
        #region ImageSource Property
        // Using a DependencyProperty as the backing store for ImageSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.RegisterAttached(
                "ImageSource",
                typeof(ImageSource),
                typeof(DataGridAttachProperties),
                new PropertyMetadata(null));
        public static ImageSource GetImageSource(DependencyObject obj)
        {
            return (ImageSource)obj.GetValue(ImageSourceProperty);
        }

        public static void SetImageSource(DependencyObject obj, ImageSource value)
        {
            obj.SetValue(ImageSourceProperty, value);
        }
        #endregion

        #region CustomSorter Property
        public static readonly DependencyProperty CustomSorterProperty =
            DependencyProperty.RegisterAttached(
                "CustomSorter", 
                typeof(ICustomSorter), 
                typeof(DataGridAttachProperties));

        public static ICustomSorter GetCustomSorter(DataGridColumn gridColumn)
        {
            return (ICustomSorter)gridColumn.GetValue(CustomSorterProperty);
        }

        public static void SetCustomSorter(DataGridColumn gridColumn, ICustomSorter value)
        {
            gridColumn.SetValue(CustomSorterProperty, value);
        }

        #endregion

        #region AllowCustomSort Property
        public static readonly DependencyProperty AllowCustomSortProperty =
            DependencyProperty.RegisterAttached(
                "AllowCustomSort",
                typeof(bool),
                typeof(DataGridAttachProperties), 
                new UIPropertyMetadata(false, OnAllowCustomSortChanged));

        public static bool GetAllowCustomSort(DataGrid grid)
        {
            return (bool)grid.GetValue(AllowCustomSortProperty);
        }

        public static void SetAllowCustomSort(DataGrid grid, bool value)
        {
            grid.SetValue(AllowCustomSortProperty, value);
        }
        #endregion



        public static bool GetAllowAppliedEmptyTemplate (DependencyObject obj)
        {
            return (bool)obj.GetValue(AllowAppliedEmptyTemplateProperty);
        }

        public static void SetAllowAppliedEmptyTemplate(DependencyObject obj, bool value)
        {
            obj.SetValue(AllowAppliedEmptyTemplateProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AllowAppliedEmptyTemplateProperty =
            DependencyProperty.RegisterAttached(
                "AllowAppliedEmptyTemplate", 
                typeof(bool), 
                typeof(DataGridAttachProperties), 
                new PropertyMetadata(false, OnSettingApplyEmptyTemplate));



        private static void OnSettingApplyEmptyTemplate(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataGrid dataGrid = d as DataGrid;

            if (dataGrid == null)
                return;

            bool newValue = (bool)e.NewValue;

            if (newValue == true)
            {
                dataGrid.SourceUpdated += ApplyPropertyTemplate;
            }
            else
            {
                dataGrid.SourceUpdated -= ApplyPropertyTemplate;
            }
        }

        private static void ApplyPropertyTemplate(object sender, DataTransferEventArgs e)
        {
            DataGrid grid = sender as DataGrid;

            if (grid == null)
                return;

            int count = grid.Items.Count;
            e.Handled = true;


            // Empty Source
            if (count == 0)
            {
                try
                {
                    grid.Template = (ControlTemplate)Application.Current.FindResource("EmptyCollectionTemplate");
                    grid.ApplyTemplate();
                }
                catch { }
            }
            else
            {

            }
        }





        /// <summary>
        /// Handle for the event when the AllowCustomSort property changed
        /// </summary>
        /// <param name="d">The object which owns the triggered property</param>
        /// <param name="e">Information of <see cref="DependencyPropertyChangedEventHandler"/></param>
        private static void OnAllowCustomSortChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var existing = d as DataGrid;
            if (existing == null)
                return;

            var oldAllow = (bool)e.OldValue;
            var newAllow = (bool)e.NewValue;

            // the sort is not allowed at first but allowed then
            if (!oldAllow && newAllow)
            {
                existing.Sorting += HandleCustomSorting;
            }
            else
            {
                existing.Sorting -= HandleCustomSorting;
            }
        }

        private static void HandleCustomSorting(object sender, DataGridSortingEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            if (dataGrid == null || !GetAllowCustomSort(dataGrid)) return;

            var listColView = dataGrid.ItemsSource as ListCollectionView;
            if (listColView == null)
                throw new Exception("The DataGrid's ItemsSource property must be of type, ListCollectionView");

            // Sanity check
            var sorter = GetCustomSorter(e.Column);
            if (sorter == null)
                return;

            // The guts.
            e.Handled = true;



            var direction = (e.Column.SortDirection != ListSortDirection.Ascending)
                                ? ListSortDirection.Ascending
                                : ListSortDirection.Descending;

            e.Column.SortDirection = sorter.SortDirection = direction;
            listColView.CustomSort = sorter;

        }
    }
}
