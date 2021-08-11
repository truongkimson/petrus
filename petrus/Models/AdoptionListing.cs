using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace petrus.Models
{
    public class AdoptionListing
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string AdoptionId { get; set; }
        [Required]
        public string Species { get; set; }
        [Required]
        public string Name { get; set; }
        public int Age { get; set; }
        [Required]
        public string Breed1 { get; set; }
        public string Breed2 { get; set; }
        [Required]
        public string  Gender { get; set; }
        [Required]
        public string Color1 { get; set; }
        public string Color2 { get; set; }
        public string Color3 { get; set; }
        public string MaturitySize { get; set; }
        [Required]
        public string FurLength { get; set; }
        public bool Vaccinated { get; set; }
        public bool Dewormed { get; set; }
        public bool Sterilized { get; set; }
        public string Health { get; set; }
        public int QuantityRepresented { get; set; }
        public double Fee { get; set; }
        public byte[] VideoBytes { get; set; }
        public byte[] ImageBytes { get; set; }
        public string Description { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime ListingDate { get; set; }
        public string ApplicationStatus { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<AdoptionRequest> AdoptionRequests { get; set; }
    }
}
