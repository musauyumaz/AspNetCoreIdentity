using AspNetCoreIdentityApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Persistence.Identity
{
    public sealed class PasswordValidator : IPasswordValidator<User>
    {
        public async Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user, string? password)
        {
            var errors = new List<IdentityError>();
            if (password.ToLower().Contains(user.UserName.ToLower()))
            {
                errors.Add(new() { Code = "PasswordContainUserName", Description = "Şifre alanı kullanıcı adı içeremez" });
            }

            if (password.ToLower().StartsWith("1234"))
            {
                errors.Add(new() { Code = "PasswordStartWith1234", Description="Şifre alanı 1234 ile başlayamaz" });
            }

            return await Task.FromResult(errors.Count == 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));
        }
    }
}
