using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.MVC.Models.DTOs
{
    public record ForgetPasswordRequestDTO
    {
        [Required(ErrorMessage = "Email alanı boş bırakılamaz")]
        [EmailAddress(ErrorMessage = "Geçersiz Email Adresi")]
        [Display(Name = "Email :")]
        public string Email { get; init; }
    }
}
