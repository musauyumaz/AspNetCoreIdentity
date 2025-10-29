using AspNetCoreIdentityApp.Application.Commons.Results;
using AspNetCoreIdentityApp.Domain.Entities;
using Mediator;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Application.Features.Users.Commands.ChangePassword
{
    public record UserChangePasswordCommandRequest(string Username, string OldPassword, string NewPassword) : IRequest<Result<string>>;
    public sealed class UserChangePasswordCommandHandler(UserManager<User> _userManager) : IRequestHandler<UserChangePasswordCommandRequest, Result<string>>
    {
        public async ValueTask<Result<string>> Handle(UserChangePasswordCommandRequest request, CancellationToken cancellationToken)
        {
            User? hasUser = await _userManager.FindByNameAsync(request.Username);
            if (hasUser is null)
                return Result<string>.Fail("Kullanıcı Bulunamadı", System.Net.HttpStatusCode.NotFound);

            IdentityResult? result = await _userManager.ChangePasswordAsync(hasUser, request.OldPassword, request.NewPassword);

            return result.Succeeded
                ? Result<string>.Success("Şifre değiştirme işlemi başarılı.")
                : Result<string>.Fail($"Şifre değiştirme işlemi başarısız oldu. Lütfen bilgilerinizi kontrol ediniz. {string.Join(",\n", result.Errors.Select(e => e.Description))}");
        }
    }
}
