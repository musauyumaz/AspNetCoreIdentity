namespace AspNetCoreIdentityApp.MVC.Models.DTOs
{
    public record ResetPasswordRequestDTO(string UserId, string Token, string Password);

}
