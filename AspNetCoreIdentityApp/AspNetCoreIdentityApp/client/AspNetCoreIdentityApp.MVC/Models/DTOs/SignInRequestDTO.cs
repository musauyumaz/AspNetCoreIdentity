using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.MVC.Models.DTOs
{
    public record SignInRequestDTO
    {
        [Required(ErrorMessage = "Kullanıcı Ad Email alanı boş bırakılamaz")]
        [Display(Name = "Kullanıcı Ad veya Email")]
        public string UserNameOrEmail { get; init; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre alanı boş bırakılamaz")]
        [Display(Name = "Şifre")]
        public string Password { get; init; }

        [Display(Name = "Beni Hatırla")]
        public bool RememberMe { get; set; }
    }
}