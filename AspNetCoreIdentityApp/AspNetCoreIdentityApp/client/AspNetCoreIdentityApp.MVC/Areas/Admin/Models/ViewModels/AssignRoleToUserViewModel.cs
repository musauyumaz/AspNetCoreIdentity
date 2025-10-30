using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.MVC.Areas.Admin.Models.ViewModels
{
    public record AssignRoleToUserViewModel
    {
        public string RoleId { get; set; }
        public string Name { get; set; }
        public bool Exists { get; set; }
    }
}
