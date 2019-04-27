using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIProject.Test
{
    public class Customer
    {
        public string CustomerID { get; set; } = "1";
        public string Name { get; set; } = "Nguyen Van A";
        public string Gender { get; set; } = "Nu";
        public string Address { get; set; } = "123 Duong so 6, Phuong Linh Trung, Quan Thu Duc, TP HCM";
        public string Area { get; set; } = "Quan Thu Duc, TP HCM";
        public string Email { get; set; } = "unknown@gmail.com";
        public string PhoneNumber { get; set; } = "0337417742";

        public long Debt { get; set; } = 12550000;

        public Customer() : base()
        {

        }
    }
}
