using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject.ExtensionFunctions
{
    public interface INgayLap
    {
        DateTime NgayLapDateTime { get; set; }
        string NgayLapDate { get; }
        string NgayLapTime { get; }
    }
}
