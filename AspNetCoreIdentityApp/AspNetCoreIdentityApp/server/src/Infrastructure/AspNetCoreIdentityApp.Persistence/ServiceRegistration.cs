using AspNetCoreIdentityApp.Domain.Entities;
using AspNetCoreIdentityApp.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreIdentityApp.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        var sql = configuration.GetConnectionString("SQLConnection");
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SQLConnection")));
        services.AddIdentity<User, Role>().AddEntityFrameworkStores<AppDbContext>();
    }
}
