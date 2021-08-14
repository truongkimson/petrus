using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace petrus.Models
{
    public class ServiceBooking
    {
        [Key]
        [Required]
        [Display(Name = "ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ServiceBookingID { get; set; }
        public string SpecialRequest { get; set; }
        [Required]
        public string RequestedDate { get; set; }
        [Required]
        public string PetName { get; set; }
        [Required]
        public int Age { get; set; }
        [Required(ErrorMessage = "Please enter breed")]
        public Breed Breed { get; set; }
        [Required(ErrorMessage = "Please enter gender")]
        
        public string UserID { get; set; }
        public virtual ServiceListing ServiceListing { get; set; }

    }
   
    
    
}
