using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.MVC.Models
{
    public record ResetPasswordViewModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre alanı boş bırakılamaz")]
        [Display(Name = "Şifre :")]
        public string Password { get; init; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Şifreler Uyuşmuyor")]
        [Required(ErrorMessage = "Şifre Tekrar alanı boş bırakılamaz")]
        [Display(Name = "Şifre Tekrar :")]
        public string PasswordConfirm { get; init; }
    }
}
