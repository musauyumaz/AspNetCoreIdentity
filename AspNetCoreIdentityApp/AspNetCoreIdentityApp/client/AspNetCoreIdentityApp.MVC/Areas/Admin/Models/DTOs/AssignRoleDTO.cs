namespace AspNetCoreIdentityApp.MVC.Areas.Admin.Models.DTOs
{
    public record AssignRoleDTO
    {
        public string UserId { get; set; }
        public List<string> SelectedRoles { get; set; } = new List<string>();
    }
}
