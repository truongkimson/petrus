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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string AdoptionRequestId { get; set; }
        public string Description { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime RequestDate { get; set; }
        public Residence residenceType { get; set; }
        public RequestStatus requestStatus { get; set; }
        public int dogsOwned { get; set; }

        public virtual User User { get; set; }
        public virtual AdoptionListing AdoptionListing { get; set; }

    }

    public enum Residence
    {
        HDB, Private
    }

    public enum RequestStatus
    {
        Accepted,Rejected
    }

}
