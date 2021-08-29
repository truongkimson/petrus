using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using petrus.Models;

namespace petrus.Security
{
    public class MyUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, IdentityRole>
    {
        public MyUserClaimsPrincipalFactory(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, 
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("DisplayName", user.Name));
            identity.AddClaim(new Claim("UserId", user.Id));
            return identity;
        }
    }
}
