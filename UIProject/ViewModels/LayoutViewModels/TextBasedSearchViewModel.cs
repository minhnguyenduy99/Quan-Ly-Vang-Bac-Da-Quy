using BaseMVVM_Service.BaseMVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIProject.Events;
using UIProject.ViewModels.FunctionInterfaces;

namespace UIProject.ViewModels.LayoutViewModels
{
    /// <summary>
    /// A view model for basic text-search
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TextBasedSearchViewModel<T> : BaseViewModelObject<T>, ISearcher<T>
    {
        public SearchMode SearchMode { get; set; }
        public virtual string Text
        {
            get => GetPropertyValue<string>();
            set
            {
                string temp = GetPropertyValue<string>();
                SetProperty(value);
                if (temp?.Equals(value) == true)
                    return;

                // Raise the base handler of TextChanged event
                OnTextPropertyChanged(new TextValueChangedEventArgs(temp, value));
             
            }
        }

        public event EventHandler<TextValueChangedEventArgs> TextChanged;

        public TextBasedSearchViewModel() : this(SearchMode.Exactly) { }  
        public TextBasedSearchViewModel(SearchMode searchMode) : base()
        {
            this.SearchMode = searchMode;
        }


        public IEnumerable<T> Search(IEnumerable<T> source, string propertyName)
        {
            if (string.IsNullOrEmpty(Text))
            {
                return source;
            }
            List<T> searchResult = new List<T>();
            foreach (var obj in source)
            {
                object propertyValue = ObservableObject.GetPropValue(obj, propertyName);
                if (OnSearchResultMatch(propertyValue.ToString(), SearchMode))
                {
                    searchResult.Add(obj);
                }
            }
            return searchResult;
        }


        /// <summary>
        /// Defines how value of a property match with the Text property
        /// </summary>
        /// <param name="propertyValue">The value of property</param>
        /// <param name="searchMode">The search mode indicates how the matching works</param>
        /// <returns></returns>
        protected virtual bool OnSearchResultMatch(string propertyValue, SearchMode searchMode)
        {
            switch (searchMode)
            {
                case SearchMode.Exactly: return propertyValue.Equals(Text);
                case SearchMode.Likely: return propertyValue.Contains(Text);
                case SearchMode.LikelyIgnoreCase: return propertyValue.ToLower().Contains(Text?.ToLower());
                default:
                    return false;
            }
        }
        protected virtual void OnTextPropertyChanged(TextValueChangedEventArgs e)
        {
            TextChanged?.Invoke(this, e);
        }

        protected override void LoadComponentsInternal()
        {
            Text = string.Empty;
        }
        protected override void ReloadComponentsInternal()
        {
            Text = string.Empty;
        }
    }
}
