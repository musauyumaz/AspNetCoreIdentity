using AspNetCoreIdentityApp.Application.Commons.Results;
using AspNetCoreIdentityApp.Domain.Entities;
using Mapster;
using Mediator;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Application.Features.Users.Commands.Update
{
    public record UserUpdateCommandRequest(string Id, string UserName, string Email, string PhoneNumber, DateTime BirthDate, string City, string Picture, byte Gender) : IRequest<Result<string>>;
    public sealed class UserUpdateCommandHandler(UserManager<User> _userManager) : IRequestHandler<UserUpdateCommandRequest, Result<string>>
    {
        public async ValueTask<Result<string>> Handle(UserUpdateCommandRequest request, CancellationToken cancellationToken)
        {
            User? updatedUser = await _userManager.FindByIdAsync(request.Id);
            if (updatedUser == null)
                return Result<string>.Fail("Güncellemek istediğiniz kullanıcı bulunamadı.", System.Net.HttpStatusCode.NotFound);

            updatedUser.UserName = request.UserName;
            updatedUser.Email = request.Email;
            updatedUser.PhoneNumber = request.PhoneNumber;
            updatedUser.BirthDate = request.BirthDate;
            updatedUser.City = request.City;
            updatedUser.Gender = request.Gender;
            updatedUser.Picture = request.Picture;

            IdentityResult? updatedUserResult = await _userManager.UpdateAsync(updatedUser);
            if(!updatedUserResult.Succeeded)
                return Result<string>.Fail(string.Join(", ", updatedUserResult.Errors.Select(e => e.Description)), System.Net.HttpStatusCode.BadRequest);
            return Result<string>.Success("Kullanıcı bilgileri başarıyla güncellendi.", System.Net.HttpStatusCode.OK);
        }
    }
}
