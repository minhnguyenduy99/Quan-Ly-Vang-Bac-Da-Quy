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
        public ValueFilterViewModel(Func<ItemViewModel<T>,bool> filterCallback) : base(filterCallback)
        {
        }

        protected override void LoadComponentsInternal()
        {
            throw new NotImplementedException();
        }

        protected override void ReloadComponentsInternal()
        {
            throw new NotImplementedException();
        }
    }
}
