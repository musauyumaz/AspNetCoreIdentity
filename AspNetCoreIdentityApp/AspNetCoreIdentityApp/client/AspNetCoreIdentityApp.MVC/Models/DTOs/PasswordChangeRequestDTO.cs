namespace AspNetCoreIdentityApp.MVC.Models.DTOs
{
    public record PasswordChangeRequestDTO(string Username, string OldPassword, string NewPassword);
}
