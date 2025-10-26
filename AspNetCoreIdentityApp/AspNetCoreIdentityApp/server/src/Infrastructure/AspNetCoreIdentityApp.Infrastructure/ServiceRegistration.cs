using AspNetCoreIdentityApp.Application.Abstractions.Services;
using AspNetCoreIdentityApp.Infrastructure.Services.Email;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreIdentityApp.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();
        }
    }
}
