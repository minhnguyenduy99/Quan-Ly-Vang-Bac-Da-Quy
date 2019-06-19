using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using UIProject.Events;

namespace UIProject.ViewModels.FunctionInterfaces
{
    /// <summary>
    /// The search mode used in <see cref="ISearcher{T}"/>
    /// </summary>
    public enum SearchMode
    {
        /// <summary>
        /// The search result requires the match exactly
        /// </summary>
        Exactly = 0,
        /// <summary>
        /// The search result requires to be likely 
        /// </summary>
        Likely = 1,
        /// <summary>
        /// The search result requires to be likely without letter case
        /// </summary>
        LikelyIgnoreCase = 2,
    }

    /// <summary>
    /// Provides searching functionality
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISearcher
    {
        SearchMode SearchMode { get; set; }
        string Text { get; set; }

        event EventHandler<TextValueChangedEventArgs> TextChanged;
    }


    /// <summary>
    ///  A generic version of <see cref="ISearcher"/>
    /// </summary>
    /// <typeparam name="T">The type of data used in the searching</typeparam>
    public interface ISearcher<T> : ISearcher
    {
        IEnumerable<T> Search(IEnumerable<T> source, string propertyName);
    }
}
