using AspNetCoreIdentityApp.Domain.Entities.Commons;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Domain.Entities;

public sealed class User : IdentityUser, ICreated, IUpdated, IIsActive
{
    public string? City { get; set; }
    public string? Picture { get; set; }
    public DateTime? BirthDate { get; set; }
    public byte? Gender { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
    public bool IsActive { get; set; }
}