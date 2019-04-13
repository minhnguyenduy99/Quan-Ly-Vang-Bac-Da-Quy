using BaseMVVM_Service.BaseMVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject.Models
{
    /// <summary>
    /// Thông tin cá nhân của nhân viên
    /// </summary>
    public class NhanVienModel : BaseDataModel
    {
        private string fullName;
        private DateTime dateOfBirth;
        private string cmnd;

        public string FullName
        {
            get => fullName;
            set => SetProperty(ref fullName, value);
        }

        public DateTime DateOfBirth
        {
            get => dateOfBirth;
            set => SetProperty(ref dateOfBirth, value);
        }

        public string CMND
        {
            get => cmnd;
            set => SetProperty(ref cmnd, value);
        }
    }
}
