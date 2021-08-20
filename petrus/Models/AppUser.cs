using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace petrus.Models
{
    public class AppUser:IdentityUser
    {
        public string LoginName { get; set; }
        public string RealName { get; set; }
    }
}
