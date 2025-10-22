using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Persistence.Identity.Localizations
{
    public sealed class LocalizationIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateUserName(string userName)
        {
            return new() { Code = "DuplicateUserName", Description = $"Kullanıcı adı sistemde zaten kayıtlı" };
            //return base.DuplicateUserName(userName);
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new() { Code = "DuplicateEmail", Description = $"Email sistemde zaten kayıtlı" };

            //return base.DuplicateEmail(email);
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new() { Code = "PasswordTooShort", Description = $"Şifre alanı en az {length} karakter olmalıdır" };
            //return base.PasswordTooShort(length);
        }
    }
}
