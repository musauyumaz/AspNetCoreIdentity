using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreIdentityApp.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediator(options => options.ServiceLifetime = ServiceLifetime.Scoped);
            return services;
        }
    }
}
