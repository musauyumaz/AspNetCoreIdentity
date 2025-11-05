using AspNetCoreIdentityApp.Application.Commons.Results;
using AspNetCoreIdentityApp.Application.Features.Users.DTOs;
using AspNetCoreIdentityApp.Domain.Entities;
using Mapster;
using Mediator;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Application.Features.Auths.Commands.SignUp
{
    public readonly record struct AuthSignUpCommandRequest(string UserName, string Email, string PhoneNumber, string Password, string PasswordConfirm) : IRequest<Result<UserDTO>>;
    public sealed class AuthSignUpCommandHandler(UserManager<User> _userManager) : IRequestHandler<AuthSignUpCommandRequest, Result<UserDTO>>
    {
        public async ValueTask<Result<UserDTO>> Handle(AuthSignUpCommandRequest request, CancellationToken cancellationToken)
        {
            var user = request.Adapt<User>();   
            var data = await _userManager.CreateAsync(user, request.PasswordConfirm);

            if (!data.Succeeded)
            {
                Result<UserDTO>.Fail(string.Join(",\n", data.Errors.Select(e => e.Description)));
            }

            var claimResult = await _userManager.AddClaimsAsync(user, new List<System.Security.Claims.Claim>
            {
                new System.Security.Claims.Claim("ExchangeExpireDate", DateTime.Now.AddDays(10).ToString())
            });

            return claimResult.Succeeded
                ? Result<UserDTO>.Success(user.Adapt<UserDTO>())
                : Result<UserDTO>.Fail(string.Join(",\n", claimResult.Errors.Select(e => e.Description)));
        }
    }
}