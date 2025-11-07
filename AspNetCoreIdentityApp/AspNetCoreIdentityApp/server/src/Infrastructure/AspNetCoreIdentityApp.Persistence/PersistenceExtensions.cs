using AspNetCoreIdentityApp.Domain.Entities;
using AspNetCoreIdentityApp.Persistence.Identity.Seeds;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreIdentityApp.Persistence
{
    public static class PersistenceExtensions
    {
        public static async Task SeedDataAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
            await PermissionSeed.Seed(roleManager);
        }
    }
}
