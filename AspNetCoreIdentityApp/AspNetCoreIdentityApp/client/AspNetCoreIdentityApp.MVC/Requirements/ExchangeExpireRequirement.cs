using Microsoft.AspNetCore.Authorization;

namespace AspNetCoreIdentityApp.MVC.Requirements
{
    public class ExchangeExpireRequirement : IAuthorizationRequirement
    {
    }

    public class ExchangeExpireRequirementHandler : AuthorizationHandler<ExchangeExpireRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ExchangeExpireRequirement requirement)
        {
            var hasExchangeExpireClaim = context.User.HasClaim(c => c.Type == "ExchangeExpireDate");

            if (!hasExchangeExpireClaim)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var exchangeExpireDate = context.User.FindFirst("ExchangeExpireDate")?.Value;

            if (DateTime.TryParse(exchangeExpireDate, out DateTime result) && DateTime.Now > result)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
