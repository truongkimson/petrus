using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace petrus.Models
{
    public class User
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string UserId { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int PhoneNumber { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        public ICollection<string> SearchTerms { get; set; }

        public virtual ICollection<AdoptionListing> AdoptionListings { get; set; }
        public virtual ICollection<AdoptionRequest> AdoptionRequests { get; set; }
    }
}
