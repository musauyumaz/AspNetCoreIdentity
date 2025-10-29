using AspNetCoreIdentityApp.Application.Commons.Results;
using AspNetCoreIdentityApp.Domain.Entities;
using Mapster;
using Mediator;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Application.Features.Roles.Commands.Add
{
    public record RoleAddCommandRequest(string Name) : IRequest<Result<string>>;
    public sealed class RoleAddCommandHandler(RoleManager<Role> _roleManager) : IRequestHandler<RoleAddCommandRequest, Result<string>>
    {
        public async ValueTask<Result<string>> Handle(RoleAddCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _roleManager.CreateAsync(request.Adapt<Role>());

            return result.Succeeded
                ? Result<string>.Success("Rol başarıyla eklendi.")
                : Result<string>.Fail(string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }
}
