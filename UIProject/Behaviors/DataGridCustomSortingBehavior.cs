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
using UIProject.ServiceProviders;

namespace UIProject.Behaviors
{
    public class DataGridCustomSortingBehavior
    {
        public static readonly DependencyProperty CustomSorterProperty =
            DependencyProperty.RegisterAttached(
                "CustomSorter", 
                typeof(ICustomSorter), 
                typeof(DataGridCustomSortingBehavior));

        public static ICustomSorter GetCustomSorter(DataGridColumn gridColumn)
        {
            return (ICustomSorter)gridColumn.GetValue(CustomSorterProperty);
        }

        public static void SetCustomSorter(DataGridColumn gridColumn, ICustomSorter value)
        {
            gridColumn.SetValue(CustomSorterProperty, value);
        }

        public static readonly DependencyProperty AllowCustomSortProperty =
            DependencyProperty.RegisterAttached(
                "AllowCustomSort",
                typeof(bool),
                typeof(DataGridCustomSortingBehavior), 
                new UIPropertyMetadata(false, OnAllowCustomSortChanged));

        public static bool GetAllowCustomSort(DataGrid grid)
        {
            return (bool)grid.GetValue(AllowCustomSortProperty);
        }

        public static void SetAllowCustomSort(DataGrid grid, bool value)
        {
            grid.SetValue(AllowCustomSortProperty, value);
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
