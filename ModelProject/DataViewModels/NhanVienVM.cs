using BaseMVVM_Service.BaseMVVM;
using ModelProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject.DataViewModels
{
    /// <summary>
    /// Cung cấp View Model cho lớp nhân viên
    /// </summary>
    public class NhanVienVM : BaseDataViewModel<NhanVienModel>
    {
        public NhanVienVM(NhanVienModel nhanVienModel):base(nhanVienModel)
        {
            
        }
    }
}
