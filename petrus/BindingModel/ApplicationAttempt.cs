﻿using petrus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace petrus.BindingModel
{
    public class ApplicationAttempt
    {
        public string userId { get; set; }
        public string adoptionListingId { get; set; }
        public string residenceType { get; set; }
        public int dogsOwned { get; set; }
        public string description { get; set; }
    }
}
