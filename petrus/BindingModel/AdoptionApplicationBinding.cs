using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using petrus.Models;

namespace petrus.BindingModel
{
    public class AdoptionApplicationBinding
    {
        public Residence residenceType { get; set; }
        public int dogsOwned { get; set; }
        public string listingId { get; set; }
        public string description { get; set; }

    }
}

