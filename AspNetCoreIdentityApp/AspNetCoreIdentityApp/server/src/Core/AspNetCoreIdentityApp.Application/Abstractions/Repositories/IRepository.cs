using AspNetCoreIdentityApp.Domain.Entities.Commons;

namespace AspNetCoreIdentityApp.Application.Abstractions.Repositories
{
    public sealed class IRepository<T> where T : BaseEntity
    {
    }
}
