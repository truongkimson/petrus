using Microsoft.AspNetCore.Http;
using petrus.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace petrus.ViewModels
{
    public class AdoptionListingViewModel
    {
        [Required(ErrorMessage = "Please enter species")]
        public Species Species { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter age")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Please enter breed")]
        [Display(Name = "Primary Breed")]
        public Breed Breed1 { get; set; }
        [Display(Name = "Secondary Breed")]
        public Breed? Breed2 { get; set; }
        [Required(ErrorMessage = "Please enter gender")]
        public Gender Gender { get; set; }
        [Required(ErrorMessage = "Please enter colour")]
        [Display(Name = "Colour 1")]
        public Color Color1 { get; set; }
        [Display(Name = "Colour 2 (optional)")]
        public Color? Color2 { get; set; }
        [Display(Name = "Colour 3 (optional)")]
        public Color? Color3 { get; set; }
        [Required(ErrorMessage = "Please enter mature size of pet")]
        [Display(Name = "Size at maturity")]
        public MaturitySize MaturitySize { get; set; }
        [Required(ErrorMessage = "Please enter fur length of pet")]
        [Display(Name = "FurLength")]
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
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Please enter adoption fee")]
        public double Fee { get; set; }
        public string Description { get; set; }
        [Display(Name = "Pet Image")]
        public IFormFile PetImage { get; set; }
        [Display(Name = "Pet Video")]
        public IFormFile PetVideo { get; set; }
    }
}
