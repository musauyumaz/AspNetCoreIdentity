using AspNetCoreIdentityApp.Application.Commons.Results;
using AspNetCoreIdentityApp.Application.Features.Auths.DTOs;
using AspNetCoreIdentityApp.Domain.Entities;
using Mapster;
using Mediator;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Application.Features.Auths.Commands.SignIn
{
    public readonly record struct SignInAuthCommandRequest(string UserNameOrEmail,string Password) : IRequest<Result<UserDTO>>;

    public sealed class SignInAuthCommandHandler(SignInManager<User> _signInManager) : IRequestHandler<SignInAuthCommandRequest, Result<UserDTO>>
    {
        public async ValueTask<Result<UserDTO>> Handle(SignInAuthCommandRequest request, CancellationToken cancellationToken)
        {
            User? hasUser = await _signInManager.UserManager.FindByNameAsync(request.UserNameOrEmail) ?? await _signInManager.UserManager.FindByEmailAsync(request.UserNameOrEmail);

            if (hasUser is null)
                return Result<UserDTO>.Fail("Kullanıcı Adı veya Email Yanlış!");

            SignInResult? result = await _signInManager.PasswordSignInAsync(hasUser,request.Password, false,false);

            return result.Succeeded
                ? Result<UserDTO>.Success(hasUser.Adapt<UserDTO>())
                : Result<UserDTO>.Fail("Kullanıcı Adı veya Email Yanlış!");
        }
    }
}
