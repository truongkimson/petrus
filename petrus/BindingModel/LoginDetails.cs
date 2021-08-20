using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace petrus.BindingModel
{
    public class LoginDetails
    {
        [Required(ErrorMessage = "EmailAddress can not be null")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Password can not be null")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
