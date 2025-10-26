using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.MVC.Models.DTOs
{
    public record SignUpRequestDTO
    {
        [Required(ErrorMessage ="Kullanıcı Ad alanı boş bırakılamaz")]
        [Display(Name = "Kullanıcı Adı :")]
        public string UserName { get; init; }

        [Required(ErrorMessage ="Email alanı boş bırakılamaz")]
        [EmailAddress(ErrorMessage ="Geçersiz Email Adresi")]
        [Display(Name = "Email :")]
        public string Email { get; init; }

        [Required(ErrorMessage ="Telefon alanı boş bırakılamaz")]
        [Display(Name = "Telefon :")]
        public string PhoneNumber { get; init; }

        [Required(ErrorMessage ="Şifre alanı boş bırakılamaz")]
        [Display(Name = "Şifre :")]
        [DataType(DataType.Password)]
        public string Password { get; init; }

        [Compare(nameof(Password), ErrorMessage ="Şifreler Uyuşmuyor")]
        [Required(ErrorMessage ="Şifre Tekrar alanı boş bırakılamaz")]
        [Display(Name = "Şifre Tekrar :")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; init; }
    }
}
