using Microsoft.AspNetCore.Http;
using petrus.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace petrus.ViewModels
{
    public class ServiceListingViewModel
    {
        [Required(ErrorMessage = "Please enter species")]
        public Species Species { get; set; }
        [Required(ErrorMessage ="Please enter name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter price")]
        public string Price { get; set; }
        [Required(ErrorMessage = "Please enter category")]
        public string ServiceCategory{ get; set; }
        [Required(ErrorMessage = "Please enter email address")]
        public string Email { get; set; }
        public string Qualifications { get; set; }
        [Required(ErrorMessage ="Please enter available days")]
        public List<DateTime> AvailableDate { get; set; }
        public string Description { get; set; }
        [Display(Name = "Pet Image")]
        public IFormFile PetImage { get; set; }
        [Display(Name = "Pet Video")]
        public IFormFile PetVideo { get; set; }
    }
}
