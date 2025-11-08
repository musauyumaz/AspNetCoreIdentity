using System.Security.Claims;
using AspNetCoreIdentityApp.Domain.Entities;
using AspNetCoreIdentityApp.Persistence.Identity.Permissions;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Persistence.Identity.Seeds;

public sealed class PermissionSeed
{
    private const string BasicRoleName = "BasicRole";
    private const string AdvancedRoleName = "AdvancedRole";
    private const string AdminRoleName = "AdminRole";

    public static async Task Seed(RoleManager<Role> roleManager)
    {
        await CreateRoleWithPermissions(roleManager, BasicRoleName, PermissionLevel.Read);
        await CreateRoleWithPermissions(roleManager, AdvancedRoleName, PermissionLevel.ReadWriteUpdate);
        await CreateRoleWithPermissions(roleManager, AdminRoleName, PermissionLevel.Full);
    }

    private static async Task CreateRoleWithPermissions(
        RoleManager<Role> roleManager,
        string roleName,
        PermissionLevel level)
    {
        if (await roleManager.RoleExistsAsync(roleName))
            return;

        var role = new Role { Name = roleName };
        var result = await roleManager.CreateAsync(role);

        if (!result.Succeeded)
            throw new InvalidOperationException(
                $"Role '{roleName}' oluşturulamadı: {string.Join(", ", result.Errors.Select(e => e.Description))}");

        await AssignPermissions(roleManager, role, level);
    }

    private static async Task AssignPermissions(
        RoleManager<Role> roleManager,
        Role role,
        PermissionLevel level)
    {
        var permissions = GetPermissionsForLevel(level);

        foreach (var permission in permissions)
        {
            await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
        }
    }

    private static IEnumerable<string> GetPermissionsForLevel(PermissionLevel level)
    {
        var permissions = new List<string>();

        if (level >= PermissionLevel.Read)
        {
            permissions.AddRange(new[]
            {
                Permission.Stock.Read,
                Permission.Order.Read,
                Permission.Catalog.Read
            });
        }

        if (level >= PermissionLevel.ReadWriteUpdate)
        {
            permissions.AddRange(new[]
            {
                Permission.Stock.Create,
                Permission.Order.Create,
                Permission.Catalog.Create,
                Permission.Stock.Update,
                Permission.Order.Update,
                Permission.Catalog.Update
            });
        }

        if (level >= PermissionLevel.Full)
        {
            permissions.AddRange(new[]
            {
                Permission.Stock.Delete,
                Permission.Order.Delete,
                Permission.Catalog.Delete
            });
        }

        return permissions;
    }

    private enum PermissionLevel
    {
        Read = 1,
        ReadWriteUpdate = 2,
        Full = 3
    }
}