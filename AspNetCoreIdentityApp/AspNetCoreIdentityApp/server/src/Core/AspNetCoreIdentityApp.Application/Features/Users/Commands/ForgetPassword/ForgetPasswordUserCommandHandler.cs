using AspNetCoreIdentityApp.Application.Abstractions.Services;
using AspNetCoreIdentityApp.Application.Commons.Results;
using AspNetCoreIdentityApp.Domain.Entities;
using Mediator;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace AspNetCoreIdentityApp.Application.Features.Users.Commands.ForgetPassword
{
    public record ForgetPasswordUserCommandRequest(string Email) : IRequest<Result<string>>;
    public sealed class ForgetPasswordUserCommandHandler(UserManager<User> _userManager, IEmailService _emailService) : IRequestHandler<ForgetPasswordUserCommandRequest, Result<string>>
    {
        public async ValueTask<Result<string>> Handle(ForgetPasswordUserCommandRequest request, CancellationToken cancellationToken)
        {
            User? hasUser = await _userManager.FindByEmailAsync(request.Email);

            if(hasUser is null)
                return Result<string>.Fail("Kullanıcı bulunamadı", HttpStatusCode.NotFound);

            string? passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(hasUser);
            string resetLink = $"https://localhost:7160/Home/ResetPassword?userId={hasUser.Id}&token={Uri.EscapeDataString(passwordResetToken)}";

            await _emailService.SendEmailAsync(hasUser.Email,"Localhost | Şifre Sıfırlama Linki",
                body: @$"<h4>Şifrenizi yenilemek için aşağıdaki linke tıklayınız</h4>
                        <p> <a href='{resetLink}'>Şifre Yenileme Linki</a> </p>");

            return Result<string>.Success("Şifre sıfırlama linki e-posta adresinize gönderilmiştir.");
        }
    }
}
