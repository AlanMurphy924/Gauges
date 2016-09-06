using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauges
{
    public class Utilities
    {
        public static double Clamp(double v, double minimum, double maximum)
        {
            if (v > maximum)
            {
                v = maximum;
            }

            if (v < minimum)
            {
                v = minimum;
            }

            return v;
        }
    }
}
