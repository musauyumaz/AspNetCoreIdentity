using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.MVC.Models
{
    public record ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Email alanı boş bırakılamaz")]
        [EmailAddress(ErrorMessage = "Geçersiz Email Adresi")]
        [Display(Name = "Email :")]
        public string Email { get; init; }
    }
}
