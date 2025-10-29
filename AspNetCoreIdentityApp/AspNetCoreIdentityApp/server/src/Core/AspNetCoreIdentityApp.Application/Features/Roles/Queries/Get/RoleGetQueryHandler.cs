using AspNetCoreIdentityApp.Application.Commons.Results;
using AspNetCoreIdentityApp.Application.Features.Roles.DTOs;
using AspNetCoreIdentityApp.Domain.Entities;
using Mapster;
using Mediator;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Application.Features.Roles.Queries.Get
{
    public record RoleGetQueryRequest(string Id) : IRequest<Result<RoleDTO>>;
    public sealed class RoleGetQueryHandler(RoleManager<Role> _roleManager) : IRequestHandler<RoleGetQueryRequest, Result<RoleDTO>>
    {
        public async ValueTask<Result<RoleDTO>> Handle(RoleGetQueryRequest request, CancellationToken cancellationToken)
        {
            Role? data = await _roleManager.FindByIdAsync(request.Id);
            if(data == null) 
                return Result<RoleDTO>.Fail("Role not found", System.Net.HttpStatusCode.NotFound);

            return Result<RoleDTO>.Success(data.Adapt<RoleDTO>());

        }
    }
}
