using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.MVC.Models.ViewModels
{
    public record PasswordChangeViewModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Eski Şifre alanı boş bırakılamaz")]
        [Display(Name = "Eski Şifre :")]
        [MinLength(6, ErrorMessage ="Eski Şifre en az 6 karakter olmalıdır")]
        public string OldPassword { get; init; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Yeni Şifre alanı boş bırakılamaz")]
        [Display(Name = "Yeni Şifre :")]
        [MinLength(6, ErrorMessage = "Yeni Şifre en az 6 karakter olmalıdır")]
        public string NewPassword { get; init; }

        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "Şifreler Uyuşmuyor")]
        [Required(ErrorMessage = "Yeni Şifre Tekrar alanı boş bırakılamaz")]
        [Display(Name = "Yeni Şifre Tekrar :")]
        public string NewPasswordConfirm { get; init; }
    }
}
