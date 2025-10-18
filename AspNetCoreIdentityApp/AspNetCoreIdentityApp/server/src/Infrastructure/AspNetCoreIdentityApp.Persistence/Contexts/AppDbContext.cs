using AspNetCoreIdentityApp.Domain.Entities;
using AspNetCoreIdentityApp.Domain.Entities.Commons;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AspNetCoreIdentityApp.Persistence.Contexts;

public sealed class AppDbContext(DbContextOptions options) : IdentityDbContext<User, Role, string>(options)
{
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Added)
            {
                if (entry.Entity is ICreated created)
                    created.CreatedDate = now;
                if (entry.Entity is IIsActive active)
                    active.IsActive = true;
            }
            else if (entry.State == EntityState.Modified)
            {
                if (entry.Entity is IUpdated updated)
                    updated.ModifiedDate = now;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }


}
