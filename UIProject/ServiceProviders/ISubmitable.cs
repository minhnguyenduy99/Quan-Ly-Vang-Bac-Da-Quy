using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIProject.ServiceProviders
{
    public interface ISubmitable<T>
    {
        T MainModel { get; set; }
        void Submit();
    }
}
