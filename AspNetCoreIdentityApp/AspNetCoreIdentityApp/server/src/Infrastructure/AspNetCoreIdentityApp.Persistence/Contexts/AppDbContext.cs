using AspNetCoreIdentityApp.Domain.Entities;
using AspNetCoreIdentityApp.Domain.Entities.Commons;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;

namespace AspNetCoreIdentityApp.Persistence.Contexts;

public sealed class AppDbContext(DbContextOptions options) : IdentityDbContext<User, Role, string>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

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
