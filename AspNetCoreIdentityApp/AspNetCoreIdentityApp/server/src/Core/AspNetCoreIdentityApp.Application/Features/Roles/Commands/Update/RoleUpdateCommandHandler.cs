using AspNetCoreIdentityApp.Application.Commons.Results;
using AspNetCoreIdentityApp.Domain.Entities;
using Mediator;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Application.Features.Roles.Commands.Update
{
    public record RoleUpdateCommandRequest(string Id, string Name) : IRequest<Result<string>>;

    public sealed class RoleUpdateCommandHandler(RoleManager<Role> _roleManager) : IRequestHandler<RoleUpdateCommandRequest, Result<string>>
    {
        public async ValueTask<Result<string>> Handle(RoleUpdateCommandRequest request, CancellationToken cancellationToken)
        {
            Role? updatedRole = await _roleManager.FindByIdAsync(request.Id);
            if (updatedRole is null)
                return Result<string>.Fail("Role not found", System.Net.HttpStatusCode.NotFound);
            updatedRole.Name = request.Name;
            IdentityResult result = await _roleManager.UpdateAsync(updatedRole);
            return result.Succeeded
                ? Result<string>.Success("Role Güncellenmiştir", System.Net.HttpStatusCode.OK)
                : Result<string>.Fail(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)), System.Net.HttpStatusCode.BadRequest);
        }
    }
}
