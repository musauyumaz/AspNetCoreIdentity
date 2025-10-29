using AspNetCoreIdentityApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Persistence.Identity.Validations
{
    public sealed class UserValidator : IUserValidator<User>
    {
        public async Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
        {
            var errors = new List<IdentityError>();

            if(int.TryParse(user.UserName?[0].ToString(), out int number))
            {
                errors.Add(new() { Code = "UserNameStartsWithNumber", Description = $"Kullanıcı adı rakam ile başlayamaz : {number}" });
            }

            return await Task.FromResult(errors.Count == 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));
        }
    }
}
