using AspNetCoreIdentityApp.Application.Commons.Results;
using AspNetCoreIdentityApp.Application.Features.Users.DTOs;
using AspNetCoreIdentityApp.Domain.Entities;
using Mapster;
using Mediator;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Application.Features.Users.Queries.GetAll
{
    public sealed record UserGetAllQueryRequest() : IRequest<Result<List<UserDTO>>>;
    public sealed class UserGetAllQueryHandler(UserManager<User> _userManager) : IRequestHandler<UserGetAllQueryRequest, Result<List<UserDTO>>>
    {
        public async ValueTask<Result<List<UserDTO>>> Handle(UserGetAllQueryRequest request, CancellationToken cancellationToken)
        {
            List<UserDTO>? data = _userManager.Users.ProjectToType<UserDTO>().ToList();
            return Result<List<UserDTO>>.Success(data);
        }
    }
}
