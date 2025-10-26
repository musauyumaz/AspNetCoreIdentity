using AspNetCoreIdentityApp.Application.Commons.Results;
using AspNetCoreIdentityApp.Application.Features.Users.DTOs;
using AspNetCoreIdentityApp.Domain.Entities;
using Mapster;
using Mediator;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Application.Features.Users.Queries.GetAll
{
    public sealed record GetAllUsersQueryRequest() : IRequest<Result<List<UserDTO>>>;
    public sealed class GetAllUsersQueryHandler(UserManager<User> _userManager) : IRequestHandler<GetAllUsersQueryRequest, Result<List<UserDTO>>>
    {
        public async ValueTask<Result<List<UserDTO>>> Handle(GetAllUsersQueryRequest request, CancellationToken cancellationToken)
        {
            List<UserDTO>? data = _userManager.Users.ProjectToType<UserDTO>().ToList();
            return Result<List<UserDTO>>.Success(data);
        }
    }
}
