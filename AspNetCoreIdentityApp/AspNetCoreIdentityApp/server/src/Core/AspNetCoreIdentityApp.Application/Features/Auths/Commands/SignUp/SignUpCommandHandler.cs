using AspNetCoreIdentityApp.Application.Commons.Results;
using AspNetCoreIdentityApp.Application.Features.Auths.DTOs;
using AspNetCoreIdentityApp.Domain.Entities;
using Mapster;
using Mediator;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Application.Features.Auths.Commands.SignUp
{
    public readonly record struct SignUpCommandRequest(string UserName, string Email, string PhoneNumber, string Password, string PasswordConfirm) : IRequest<Result<UserDTO>>;
    public sealed class SignUpCommandHandler(UserManager<User> _userManager) : IRequestHandler<SignUpCommandRequest, Result<UserDTO>>
    {
        public async ValueTask<Result<UserDTO>> Handle(SignUpCommandRequest request, CancellationToken cancellationToken)
        {
            var data = await _userManager.CreateAsync(request.Adapt<User>(),request.PasswordConfirm);
            return data.Succeeded
                ? Result<UserDTO>.Success(request.Adapt<UserDTO>())
                : Result<UserDTO>.Fail(string.Join(", ", data.Errors.Select(e => e.Description)));
        }
    }
}