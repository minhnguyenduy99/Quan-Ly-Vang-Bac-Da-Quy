using BaseMVVM_Service.BaseMVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject.AggregationalModel
{
    public class HoaDonModel : BaseSubmitableModel
    {
        public PhieuBanModel PhieuBan
        {
            get => GetPropertyValue<PhieuBanModel>();
            set => SetProperty(value);
        }
        public List<ChiTietBanModel> DSChiTietBan
        {
            get => GetPropertyValue<List<ChiTietBanModel>>();
            set => SetProperty(value);
        }

        public KhachHangModel KhachHang
        {
            get
            {
                if (PhieuBan != null)
                    return DataAccess.LoadKHByMaKH(PhieuBan.MaKH);
                return null;
            }
            set
            {
                if (PhieuBan != null)
                    PhieuBan.MaKH = value.MaKH;
            }
        }

        public HoaDonModel() : this(null)
        {
            
        }

        public HoaDonModel(PhieuBanModel phieuBan)
        {
            this.PhieuBan = phieuBan;
            DSChiTietBan = new List<ChiTietBanModel>();
        }
        public override bool Equals(object obj)
        {
            // Define Equals method based on HoaDonModel Equals method
            if (!(obj is HoaDonModel))
                return false;
            return ((HoaDonModel)obj).PhieuBan.Equals(PhieuBan);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        protected override void Add()
        {
            DataAccess.SavePhieuBan(this.PhieuBan);
            foreach (var chitietBan in DSChiTietBan)
            {
                DataAccess.SaveChiTietBan(chitietBan);
            }
        }

        protected override void Delete()
        {
            throw new NotImplementedException();
        }

        protected override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
