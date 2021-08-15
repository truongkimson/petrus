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
        [Display(Name = "ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string AdoptionListingID { get; set; }
        [Required(ErrorMessage = "Please enter species")]
        public Species Species { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter age")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Please enter breed")]
        [Display(Name = "Primary Breed")]
        public Breed Breed1 { get; set; }
        public Breed? Breed2 { get; set; }
        [Required(ErrorMessage = "Please enter gender")]
        public Gender  Gender { get; set; }
        [Required(ErrorMessage = "Please enter colour")]
        [Display(Name = "Colour")]
        public Color Color1 { get; set; }
        public Color? Color2 { get; set; }
        public Color? Color3 { get; set; }
        [Required(ErrorMessage = "Please enter mature size of pet")]
        [Display(Name = "Size at maturity")]
        public MaturitySize MaturitySize { get; set; }
        [Required(ErrorMessage = "Please enter fur length of pet")]
        [Display(Name = "Fur Length")]
        public FurLength FurLength { get; set; }
        [Required(ErrorMessage = "Please enter vaccination status")]
        public Vaccinated Vaccinated { get; set; }
        [Required(ErrorMessage = "Please enter deworming status")]
        public Dewormed Dewormed { get; set; }
        [Required(ErrorMessage = "Please enter sterilisation status")]
        public Sterilized Sterilized { get; set; }
        [Required(ErrorMessage = "Please enter health status")]
        public Health Health { get; set; }
        [Required(ErrorMessage = "Please enter quantity of pets for this adoption listing")]
        public int QuantityRepresented { get; set; }
        [Required(ErrorMessage = "Please enter adoption fee")]
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
    public static class BreedExtensions
    {
        public static string ToFriendlyString(this Breed? something)
        {
            string output = "";
            string original = something.ToString();
            if (something != null)
            {
                foreach (char x in original)
                {
                    if (Char.IsUpper(x))
                        output += " " + x;
                    else
                        output += x;
                }
            }
            return output;
        }
    }
}
