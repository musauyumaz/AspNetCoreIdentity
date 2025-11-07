using AspNetCoreIdentityApp.Domain.Entities;
using AspNetCoreIdentityApp.Persistence.Identity.Permissions;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Persistence.Identity.Seeds;
public sealed class PermissionSeed
{
    public static async Task Seed(RoleManager<Role> roleManager)
    {
        var hasBasicRole = await roleManager.RoleExistsAsync("BasicRole");
        if (!hasBasicRole)
        {
            var basicRole = new Role { Name = "BasicRole" };
            await roleManager.CreateAsync(basicRole);

            var result = await roleManager.AddClaimAsync(basicRole, new("Permission", Permission.Stock.Read));
            await roleManager.AddClaimAsync(basicRole, new("Permission", Permission.Order.Read));
            await roleManager.AddClaimAsync(basicRole, new("Permission", Permission.Catalog.Read));
        }
    }
}
