using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace petrus.BindingModel
{
    public class UserDetails
    {
        public string Id { get; set; }
        public string ConcurrencyStamp { get; set; }

        [Required(AllowEmptyStrings=false)]
        [DisplayFormat(ConvertEmptyStringToNull=false)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone number")]
        [RegularExpression("[0-9]{8}", ErrorMessage="Invalid phone number")]
        public string PhoneNumber { get; set; }
    }
}
