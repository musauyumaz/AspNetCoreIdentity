using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AspNetCoreIdentityApp.MVC.Requirements
{
    public class ViolenceRequirement : IAuthorizationRequirement
    {
        public int ThresoldAge { get; set; }
    }

    public class ViolenceRequirementHandler : AuthorizationHandler<ViolenceRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ViolenceRequirement requirement)
        {
            var hasExchangeExpireClaim = context.User.HasClaim(c => c.Type == "birthdate");

            if (!hasExchangeExpireClaim)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var exchangeExpireDate = context.User.FindFirst("birthdate")?.Value;

            if (DateTime.TryParse(exchangeExpireDate, out DateTime result) && ((DateTime.Now.Year - result.Year) < requirement.ThresoldAge))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
