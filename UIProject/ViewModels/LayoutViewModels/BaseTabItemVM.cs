using BaseMVVM_Service.BaseMVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIProject.ViewModels.LayoutViewModels
{
    /// <summary>
    /// Base view model of tab item
    /// </summary>
    /// <typeparam name="T">The data model associating with <see cref="BaseTabItemVM{T}"/></typeparam>
    public abstract class BaseTabItemVM<T> : BaseViewModel<T>
    {
        private string tabName;
        private bool isSelected;


        /// <summary>
        /// Represents the name of tab
        /// </summary>
        public string TabName
        {
            get => tabName;
            set => SetProperty(ref tabName, value);
        }

        /// <summary>
        /// Indicating weither the tab is selected
        /// </summary>
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                SetProperty(ref isSelected, value);
                if (isSelected)
                    OnTabSelected(tabName);
            }
        }


        protected virtual void OnTabSelected(string tabName)
        {
            TabSelected?.Invoke(this, new TabSelectedEventArgs(tabName));
        }

        /// <summary>
        /// Event occurs when the tab is selected
        /// </summary>
        public event EventHandler<TabSelectedEventArgs> TabSelected;
    }
}
