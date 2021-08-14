using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace petrus.Models
{
    public class ServiceListing
    {
        [Key]
        [Required]
        [Display(Name = "ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ServiceListingID { get; set; }
        [Required]
        public Species Species { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Price { get; set; }
        [Required]
        public string ServiceCategory { get; set; }
        [Required]
        public List<DateTime> AvailableDate { get; set; }
        public string Qualifications { get; set; }
        public string Video { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public DateTime ListingDate { get; set; }      
        public string UserID { get; set; } 
    }
}
