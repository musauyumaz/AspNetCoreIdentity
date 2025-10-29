using AspNetCoreIdentityApp.Application.Commons.Results;
using AspNetCoreIdentityApp.Domain.Entities;
using Mediator;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Application.Features.Users.Commands.ResetPassword
{
    public record UserResetPasswordCommandRequest(string UserId, string Token, string Password) : IRequest<Result<string>>;
    public sealed class UserResetPasswordCommandHandler(UserManager<User> _userManager) : IRequestHandler<UserResetPasswordCommandRequest, Result<string>>
    {
        public async ValueTask<Result<string>> Handle(UserResetPasswordCommandRequest request, CancellationToken cancellationToken)
        {
            User? hasUser = await _userManager.FindByIdAsync(request.UserId);

            if(hasUser is null)
                return Result<string>.Fail("Kullanıcı bulunamadı");

            var result = await _userManager.ResetPasswordAsync(hasUser, request.Token, request.Password);
            await _userManager.UpdateSecurityStampAsync(hasUser);

            return result.Succeeded
                ? Result<string>.Success("Şifre sıfırlama işlemi başarılı.")
                : Result<string>.Fail($"Şifre sıfırlama işlemi başarısız oldu. Lütfen bilgilerinizi kontrol ediniz. {string.Join(",\n", result.Errors.Select(e => e.Description))}");
        }
    }
}
