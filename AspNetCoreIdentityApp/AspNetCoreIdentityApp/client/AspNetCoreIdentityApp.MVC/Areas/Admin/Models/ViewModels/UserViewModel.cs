namespace AspNetCoreIdentityApp.MVC.Areas.Admin.Models.ViewModels
{
    public record UserViewModel
    {
        public string? Id { get; init; }
        public string? UserName { get; init; }
        public string? Email { get; init; }
    }
}
