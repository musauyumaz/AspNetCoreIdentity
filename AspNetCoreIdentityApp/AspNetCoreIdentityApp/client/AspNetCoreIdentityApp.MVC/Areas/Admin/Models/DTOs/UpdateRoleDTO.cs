using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.MVC.Areas.Admin.Models.DTOs
{
    public record UpdateRoleDTO
    {
        [HiddenInput]
        public string Id { get; init; }
        [Required(ErrorMessage ="Role Adı girilmesi zorunludur.")]
        [Display(Name ="Role Adı : ")]
        public string Name { get; init; }
    }
}
