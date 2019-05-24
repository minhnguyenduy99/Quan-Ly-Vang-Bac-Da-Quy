using BaseMVVM_Service.BaseMVVM;
using Services.PrintService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace ModelProject
{
    public class BaoCaoTonKhoModel : BaseModel
    {
        public DateTime NgayBatDau
        {
            get => GetPropertyValue<DateTime>();
            set => SetProperty(value);
        }

        public DateTime NgayKetThuc
        {
            get => GetPropertyValue<DateTime>();
            set => SetProperty(value);
        }

        public List<SanPhamModel> DanhSachSanPham
        {
            get => GetPropertyValue<List<SanPhamModel>>();
            set => SetProperty(value);
        }

    }
}
