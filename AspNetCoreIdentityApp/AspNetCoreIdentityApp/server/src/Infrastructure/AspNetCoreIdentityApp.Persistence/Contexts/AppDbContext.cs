using AspNetCoreIdentityApp.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreIdentityApp.Persistence.Contexts;

public sealed class AppDbContext(DbContextOptions options) : IdentityDbContext<User, Role, string>(options)
{
}
