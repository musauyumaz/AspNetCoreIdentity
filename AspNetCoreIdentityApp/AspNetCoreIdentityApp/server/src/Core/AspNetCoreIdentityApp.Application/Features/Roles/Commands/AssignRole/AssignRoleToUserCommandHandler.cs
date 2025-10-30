using AspNetCoreIdentityApp.Application.Commons.Results;
using AspNetCoreIdentityApp.Domain.Entities;
using Mediator;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Application.Features.Roles.Commands.AssignRole
{
    public record AssignRoleToUserCommandRequest(string UserId, List<string> SelectedRoles) : IRequest<Result<string>>;
    public sealed class AssignRoleToUserCommandHandler(UserManager<User> _userManager, RoleManager<Role> _roleManager) : IRequestHandler<AssignRoleToUserCommandRequest, Result<string>>
    {
        public async ValueTask<Result<string>> Handle(AssignRoleToUserCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user == null)
                return Result<string>.Fail("Kullanıcı bulunamadı");

            var currentRoles = await _userManager.GetRolesAsync(user);

            var rolesToRemove = currentRoles.Except(request.SelectedRoles).ToList();
            if (rolesToRemove.Any())
                await _userManager.RemoveFromRolesAsync(user, rolesToRemove);

            var rolesToAdd = request.SelectedRoles.Except(currentRoles).ToList();
            if (rolesToAdd.Any())
                await _userManager.AddToRolesAsync(user, rolesToAdd);

            return Result<string>.Success("Roller başarıyla güncellendi");
        }
    }
}
