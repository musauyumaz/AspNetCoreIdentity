using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.MVC.Models
{
    public record SignInViewModel
    {
        [Required(ErrorMessage = "Kullanıcı Ad Email alanı boş bırakılamaz")]
        [Display(Name = "Kullanıcı Ad veya Email")]
        public string UserNameOrEmail { get; init; }

        [Required(ErrorMessage = "Şifre alanı boş bırakılamaz")]
        [Display(Name = "Şifre")]
        public string Password { get; init; }

        [Display(Name = "Beni Hatırla")]
        public bool RememberMe { get; set; }
    }
}