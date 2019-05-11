using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIProject.ViewModels.LayoutViewModels
{
    public class ValueFilterViewModel<T> : BaseFilterViewModel<T>
    {
        public ItemViewModel<T> Value { get; set; }
        public ValueFilterViewModel(List<Func<ItemViewModel<T>, bool>> filterCallbacks) : base(filterCallbacks)
        {
        }

    }
}
