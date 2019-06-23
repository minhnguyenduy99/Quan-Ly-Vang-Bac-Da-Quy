using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace UIProject.ViewModels.FunctionInterfaces
{
    public interface ITableConvertable
    {
        bool ConvertDataToTable(Table table);
    }
}
