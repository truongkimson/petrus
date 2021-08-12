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
        public string AdoptionListingID { get; set; }
        [Required]
        public Species Species { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public Breed Breed1 { get; set; }
        public Breed? Breed2 { get; set; }
        [Required]
        public Gender  Gender { get; set; }
        [Required]
        public Color Color1 { get; set; }
        public Color? Color2 { get; set; }
        public Color? Color3 { get; set; }
        public MaturitySize MaturitySize { get; set; }
        [Required]
        public FurLength FurLength { get; set; }
        public Vaccinated Vaccinated { get; set; }
        public Dewormed Dewormed { get; set; }
        public Sterilized Sterilized { get; set; }
        public Health Health { get; set; }
        public int QuantityRepresented { get; set; }
        public double Fee { get; set; }
        public string Video { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime ListingDate { get; set; }
        public ApplicationStatus ApplicationStatus { get; set; }
        public string UserID { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<AdoptionRequest> AdoptionRequests { get; set; }
    }
    public enum Species
    {
        Cat, Dog
    }
    public enum Breed
    {
        Affenpinscher,
        [Display(Name = "Afghan Hound")]
        AfghanHound,
        [Display(Name = "Airedale Terrier")]
        AiredaleTerrier,
        Akbash,
        Akita,
        [Display(Name = "Alaskan Malamute")]
        AlaskanMalamute,
        [Display(Name = "American Bulldog")]
        AmericanBulldog,
        [Display(Name = "American Eskimo Dog")]
        AmericanEskimoDog,
        [Display(Name = "American Hairless Terrier")]
        AmericanHairlessTerrier,
        [Display(Name = "American StaffordShire Terrier")]
        AmericanStaffordshireTerrier
    }
    public enum Gender
    {
        Male, Female, Mixed
    }
    public enum Color
    {
        Black, Brown, Golden, Yellow, Cream, Gray, White
    }
    public enum MaturitySize
    {
        Small,
        Medium,
        Large,
        [Display(Name = "Extra Large")]
        ExtraLarge,
        Unknown
    }
    public enum FurLength
    {
        Short,
        Medium,
        Long,
        Unknown
    }
    public enum Vaccinated
    {
        Yes, No, Unknown
    }
    public enum Dewormed
    {
        Yes, No, Unknown
    }
    public enum Sterilized
    {
        Yes, No, Unknown
    }
    public enum Health
    {
        Healthy,
        [Display(Name = "Minor Condition")]
        MinorCondition,
        [Display(Name = "Major Condition")]
        MajorCondition,
        Unknown
    }
    public enum ApplicationStatus
    { 
        Open,
        Closed
    }
}
