using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    class Program
    {
        static void Main(string[] args)
        {
            List<PhieuBanModel> list = new List<PhieuBanModel>();
            list = DataAccess.LoadPhieuBan();
            for (int i = 0; i < list.LongCount<PhieuBanModel>(); i++)
            {
                //Console.WriteLine(list[i].MaPhieu + " " + list[i].NgayLap);
            }
            // string to datetime type
            //DateTime d = DateTime.Parse(p.NgayLap);
            //Console.WriteLine(d.Day);
            Console.ReadKey();
        }
    }
}
