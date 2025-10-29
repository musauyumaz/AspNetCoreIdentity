using AspNetCoreIdentityApp.Application.Commons.Results;
using AspNetCoreIdentityApp.Application.Features.Roles.DTOs;
using AspNetCoreIdentityApp.Domain.Entities;
using Mapster;
using Mediator;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Application.Features.Roles.Queries.GetAll
{
    public record RoleGetAllQueryRequest : IRequest<Result<List<RoleDTO>>>;
    public sealed class RoleGetAllQueryHandler(RoleManager<Role> _roleManager) : IRequestHandler<RoleGetAllQueryRequest, Result<List<RoleDTO>>>
    {
        public async ValueTask<Result<List<RoleDTO>>> Handle(RoleGetAllQueryRequest request, CancellationToken cancellationToken)
        {
            var data = _roleManager.Roles.ProjectToType<RoleDTO>().ToList();
            return data.Any()
                ? Result<List<RoleDTO>>.Success(data)
                : Result<List<RoleDTO>>.Fail("Hiç rol bulunamadı.", System.Net.HttpStatusCode.NotFound);
        }
    }
}
