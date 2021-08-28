using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace petrus.Models
{
    public class User : IdentityUser<string>
    {
        [Required]
        public string Name { get; set; }
        [DisplayName("Search terms")]
        public string SearchTerms { get; set; }

        public virtual ICollection<AdoptionListing> AdoptionListings { get; set; }
        public virtual ICollection<AdoptionRequest> AdoptionRequests { get; set; }
    }
}
