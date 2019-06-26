using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ModelProject.ExtensionFunctions
{
    public class ToDateConverter
    {
        public DateTime Convert(string value)
        {
            if (value == null)
                return new DateTime();
            try
            {
                char[] splits = new char[] { '/', '-', ':', ' '};
                string[] dateSplit = value.ToString().Split(splits, StringSplitOptions.RemoveEmptyEntries); ;
                int month = int.Parse(dateSplit[0]);
                int day = int.Parse(dateSplit[1]);
                int year = int.Parse(dateSplit[2]);
                if (dateSplit.Length <= 3)
                {
                    return new DateTime(year, month, day);
                }
                int hour = int.Parse(dateSplit[3]);
                int minute = int.Parse(dateSplit[4]);
                int second = int.Parse(dateSplit[5]);
                string timeKind = dateSplit[6];
                if (timeKind == "PM")
                    hour = hour + 12;
                return new DateTime(year, month, day, hour, minute, second);
            }
            catch { return new DateTime(); }
        }
    }
}
