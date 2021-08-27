using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace petrus.Resources
{
    public class ChartData
    {
        public IEnumerable<string> labels { get; set; }
        public IEnumerable<int> data { get; set; }
    }
}
