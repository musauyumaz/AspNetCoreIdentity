using AspNetCoreIdentityApp.Application.Commons.Results;
using AspNetCoreIdentityApp.Domain.Entities;
using Mediator;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Application.Features.Roles.Commands.Delete
{
    public record RoleDeleteCommandRequest(string Id) : IRequest<Result<string>>;
    public sealed class RoleDeleteCommandHandler(RoleManager<Role> _roleManager) : IRequestHandler<RoleDeleteCommandRequest, Result<string>>
    {
        public async ValueTask<Result<string>> Handle(RoleDeleteCommandRequest request, CancellationToken cancellationToken)
        {
            var deletedRole = await _roleManager.FindByIdAsync(request.Id);
            if (deletedRole is null)
                return Result<string>.Fail("Silinmek istenen rol bulunamadı.", System.Net.HttpStatusCode.NotFound);

            IdentityResult result = await _roleManager.DeleteAsync(deletedRole);
            return result.Succeeded
                ? Result<string>.Success("Rol başarıyla silindi.", System.Net.HttpStatusCode.OK)
                : Result<string>.Fail("Rol silinirken bir hata oluştu: " + string.Join(", ", result.Errors.Select(e => e.Description)), System.Net.HttpStatusCode.BadRequest);
        }
    }
}
