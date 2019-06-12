using BaseMVVM_Service.BaseMVVM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIProject.ViewModels.LayoutViewModels;

namespace UIProject.ServiceProviders
{
    public class FilterHelper : BaseViewModel
    {
        public object FilterCallback { get; set; }



        public IEnumerable<T> Filter<T>(IEnumerable<T> itemsSource)
        {
            Func<T, bool> castFilter = FilterCallback as Func<T, bool>;
            return Filter(itemsSource, castFilter);
        }

        /// <summary>
        /// Filter item source with a specified filter condition
        /// </summary>
        /// <typeparam name="T">The type of item source</typeparam>
        /// <param name="itemsSource">The item source that needed to be filter</param>
        /// <param name="filterFunction">The callback represents how the item source would be filter</param>
        /// <returns></returns>
        public static IEnumerable<T> Filter<T>(IEnumerable<T> itemsSource, Func<T, bool> filterFunction)
        {
            if (itemsSource == null)
                return null;
            return itemsSource.Where(filterFunction);
        }

        public static IEnumerable<T> Filter<T>(IEnumerable<T> itemsSource, Predicate<T> filterFunction)
        {
            if (itemsSource == null)
                return null;
            Func<T, bool> filter = new Func<T, bool>(filterFunction);
            return itemsSource.Where(filter);
        }

        /// <summary>
        /// Filter the item through an array of filters
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemsSource"></param>
        /// <param name="filters">The array of filters. The filter is executed consecutively</param>
        /// <returns></returns>
        public static IEnumerable<T> Filter<T>(IEnumerable<T> itemsSource, params Func<T,bool>[] filters)
        {
            IEnumerable<T> result = itemsSource;
            for(int i=0;i<filters.Length;i++)
            {
                result = result.Where(filters[i]);
            }
            return result;
        }

        /// <summary>
        /// Filtering an items source with a collection of filters
        /// </summary>
        /// <typeparam name="T">Type of items source</typeparam>
        /// <param name="itemsSource">The items source need to be filtered </param>
        /// <param name="filters">The collection of filters</param>
        /// <returns>The filter result collection from items source</returns>
        public static IEnumerable<T> Filter<T>(IEnumerable<T> itemsSource, IEnumerable<Func<T, bool>> filters)
        {
            IEnumerable<T> result = itemsSource;
            foreach(var filter in filters)
            {
                result = result.Where(filter);
            }
            return result;
        }
    }
}
