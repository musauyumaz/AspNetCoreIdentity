using AspNetCoreIdentityApp.Application.Commons.Results;
using AspNetCoreIdentityApp.Application.Features.Users.DTOs;
using AspNetCoreIdentityApp.Domain.Entities;
using Mapster;
using Mediator;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Application.Features.Users.Queries.Get
{
    public record UserGetQueryRequest(string UserId) : IRequest<Result<UserDTO>>;
    public sealed class UserGetQueryHandler(UserManager<User> _userManager) : IRequestHandler<UserGetQueryRequest, Result<UserDTO>>
    {
        public async ValueTask<Result<UserDTO>> Handle(UserGetQueryRequest request, CancellationToken cancellationToken)
        {
            User? hasUser = await _userManager.FindByIdAsync(request.UserId);
            return hasUser is null ? Result<UserDTO>.Fail("Kullanıcı bulunamadı",System.Net.HttpStatusCode.NotFound)
                : Result<UserDTO>.Success(hasUser.Adapt<UserDTO>());
        }
    }
}
