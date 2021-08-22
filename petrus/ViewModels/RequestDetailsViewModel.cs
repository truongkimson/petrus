using petrus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace petrus.ViewModels
{
    public class RequestDetailsViewModel
    {
        public AdoptionRequest AdoptionRequest { get; set; }
        public List<AdoptionListing> AdoptionListing { get; set; }
    }
}
