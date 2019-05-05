using BaseMVVM_Service.BaseMVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIProject.Events
{
    public class WindowViewModelClosedEventArgs<T> : EventArgs where T: BaseModel
    {
        public T Model { get; private set; }    
        public WindowViewModelClosedEventArgs(T model)
        {
            Model = model;
        }
    }
}
