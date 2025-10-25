using AspNetCoreIdentityApp.Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace AspNetCoreIdentityApp.Infrastructure.Services.Email
{
    public sealed class EmailService(IConfiguration _configuration) : IEmailService
    {
        public async Task SendEmailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendEmailAsync(new[] { to }, subject, body, isBodyHtml);
        }

        public async Task SendEmailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            SmtpClient smtp = new();
            smtp.Credentials = new NetworkCredential(_configuration["Mail:SenderEmail"], _configuration["Mail:SenderPassword"]);
            smtp.Port = int.Parse(_configuration["Mail:SmtpPort"]);
            smtp.EnableSsl = true;
            smtp.Host = _configuration["Mail:SmtpServer"];

            MailMessage mail = new();
            mail.IsBodyHtml = isBodyHtml;
            foreach (string to in tos)
                mail.To.Add(new MailAddress(to));
            mail.Subject = subject;
            mail.Body = body;
            mail.From = new(_configuration["Mail:SenderEmail"], _configuration["Mail:SenderName"], Encoding.UTF8);
            await smtp.SendMailAsync(mail);
        }
    }
}
