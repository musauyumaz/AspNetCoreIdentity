using AspNetCoreIdentityApp.Application.Commons.Results;
using AspNetCoreIdentityApp.Application.Features.Users.DTOs;
using AspNetCoreIdentityApp.Domain.Entities;
using Mapster;
using Mediator;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Application.Features.Auths.Commands.SignIn
{
    public readonly record struct AuthSignInCommandRequest(string UserNameOrEmail,string Password, bool RememberMe) : IRequest<Result<UserDTO>>;

    public sealed class AuthSignInCommandHandler(SignInManager<User> _signInManager) : IRequestHandler<AuthSignInCommandRequest, Result<UserDTO>>
    {
        public async ValueTask<Result<UserDTO>> Handle(AuthSignInCommandRequest request, CancellationToken cancellationToken)
        {
            User? hasUser = await _signInManager.UserManager.FindByNameAsync(request.UserNameOrEmail) ?? await _signInManager.UserManager.FindByEmailAsync(request.UserNameOrEmail);

            if (hasUser is null)
                return Result<UserDTO>.Fail("Kullanıcı Adı/Email veya Parola Yanlış!");

            SignInResult? result = await _signInManager.CheckPasswordSignInAsync(hasUser,request.Password, request.RememberMe);
            if (result.IsLockedOut)
                return Result<UserDTO>.Fail("Hesabınız 3 dakikalığına kilitlenmiştir. Lütfen 3 dakika sonra tekrar deneyiniz.");

            return result.Succeeded
                ? Result<UserDTO>.Success(hasUser.Adapt<UserDTO>())
                : Result<UserDTO>.Fail($"Kullanıcı Adı/Email veya Parola Yanlış! Başarısız Giriş Sayısı : {hasUser.AccessFailedCount} ");
        }
    }
}
