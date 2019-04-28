using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    class TinhTrangModel
    {
        private int maTinhTrang;
        private string tenTinhTrang;

        public int MaTinhTrang { get => maTinhTrang; set => maTinhTrang = value; }
        public string TenTinhTrang { get => tenTinhTrang; set => tenTinhTrang = value; }
    }
}
