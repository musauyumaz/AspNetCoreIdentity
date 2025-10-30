using AspNetCoreIdentityApp.Application.Commons.Results;
using AspNetCoreIdentityApp.Application.Features.Roles.DTOs;
using AspNetCoreIdentityApp.Domain.Entities;
using Mediator;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Application.Features.Roles.Queries
{
    public record GetUserRolesQueryRequest(string UserId) : IRequest<Result<Dictionary<string, GetUserRoleDTO>>>;
    public sealed class GetUserRolesQueryHandler(RoleManager<Role> _roleManager, UserManager<User> _userManager) : IRequestHandler<GetUserRolesQueryRequest, Result<Dictionary<string, GetUserRoleDTO>>>
    {
        public async ValueTask<Result<Dictionary<string, GetUserRoleDTO>>> Handle(GetUserRolesQueryRequest request, CancellationToken cancellationToken)
        {
            User? user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null) 
                return Result<Dictionary<string, GetUserRoleDTO>>.Fail("Kullanıcı bulunamadı.", System.Net.HttpStatusCode.NotFound);
            List<Role>? allRoles = _roleManager.Roles.ToList();
            HashSet<string>? userRoleIds = (await _userManager.GetRolesAsync(user)).ToHashSet();

            Dictionary<string, GetUserRoleDTO>? roles = allRoles.ToDictionary(
                r => r.Id,
                r => new GetUserRoleDTO(r.Name, userRoleIds.Contains(r.Name)));
            
            return Result<Dictionary<string, GetUserRoleDTO>>.Success(new Dictionary<string, GetUserRoleDTO>(roles), System.Net.HttpStatusCode.OK);
        }
    }
}
