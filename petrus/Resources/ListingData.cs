using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace petrus.Resources
{
    public class ListingData
    {
        public string name { get; set; }
        public string species { get; set; }
        public int age { get; set; }
        public string breed { get; set; }
        public int days_elapsed { get; set; }
        public string listing_id { get; set; }
        public string img { get; set; }
    }
}
