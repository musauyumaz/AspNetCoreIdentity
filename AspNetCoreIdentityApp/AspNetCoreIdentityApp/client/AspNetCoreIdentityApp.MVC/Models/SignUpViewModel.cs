namespace AspNetCoreIdentityApp.MVC.Models
{
    public record SignUpViewModel(string UserName, string Email, string Phone, string Password, string PasswordConfirm);
}
