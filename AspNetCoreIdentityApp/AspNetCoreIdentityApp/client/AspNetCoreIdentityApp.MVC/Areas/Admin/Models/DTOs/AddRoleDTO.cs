using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.MVC.Areas.Admin.Models.DTOs
{
    public record AddRoleDTO
    {
        [Required(ErrorMessage = "Role Adı girilmesi zorunludur.")]
        [Display(Name = "Role Adı : ")]
        public string Name { get; init; }
    }
}
