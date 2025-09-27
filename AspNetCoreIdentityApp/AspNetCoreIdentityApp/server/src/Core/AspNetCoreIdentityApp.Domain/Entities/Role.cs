using AspNetCoreIdentityApp.Domain.Entities.Commons;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Domain.Entities;

public sealed class Role : IdentityRole<string>, ICreated, IUpdated, IIsActive
{
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
    public bool IsActive { get; set; }
}
