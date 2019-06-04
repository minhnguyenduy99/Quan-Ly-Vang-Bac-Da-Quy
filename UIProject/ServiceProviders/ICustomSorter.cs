using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIProject.ServiceProviders
{
    /// <summary>
    /// Interface defines how object is compared when they are in sorting
    /// </summary>
    public interface ICustomSorter : IComparer
    {
        ListSortDirection SortDirection { get; set; }
    }
}
