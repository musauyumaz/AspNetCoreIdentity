using AspNetCoreIdentityApp.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace AspNetCoreIdentityApp.Persistence.Identity.ClaimProviders
{
    public sealed class UserClaimProvider(UserManager<User> _userManager) : IClaimsTransformation
    {
        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            User? currentUser = await _userManager.GetUserAsync(principal);

            if(currentUser is null)
                return principal;

            if (!string.IsNullOrEmpty(currentUser.City) && !principal.HasClaim(x => x.Type == "city"))
            {
                Claim cityClaim = new Claim("city", currentUser.City);
                var identityUser = (ClaimsIdentity)principal.Identity!;
                identityUser.AddClaim(cityClaim);
            }

            return principal;
        }
    }
}
