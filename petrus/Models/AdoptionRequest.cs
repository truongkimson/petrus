using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace petrus.Models
{
    public class AdoptionRequest
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string AdoptionRequestId { get; set; }
        public string Description { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime RequestDate { get; set; }
        
        public virtual User User { get; set; }
        public virtual AdoptionListing AdoptionListing { get; set; }

    }
}
