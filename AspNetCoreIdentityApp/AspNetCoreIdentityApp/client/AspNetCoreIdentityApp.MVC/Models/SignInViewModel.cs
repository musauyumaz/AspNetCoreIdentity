using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.MVC.Models
{
    public record SignInViewModel
    {
        [Required(ErrorMessage = "Kullanıcı Ad Email alanı boş bırakılamaz")]
        [Display(Name = "Kullanıcı Ad veya Email :")]
        public string EmailOrUserName { get; init; }

        [Required(ErrorMessage = "Şifre alanı boş bırakılamaz")]
        [Display(Name = "Şifre :")]
        public string Password { get; init; }

        public bool RememberMe { get; set; }
    }
}
