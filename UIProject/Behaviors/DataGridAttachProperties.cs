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


            ListCollectionView listColView = new ListCollectionView(dataGrid.ItemsSource as IList);

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
