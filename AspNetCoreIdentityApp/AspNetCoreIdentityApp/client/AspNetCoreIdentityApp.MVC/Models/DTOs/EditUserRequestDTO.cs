using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.MVC.Models.DTOs
{
    public record EditUserRequestDTO
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

        [Display(Name = "Doğum Tarihi :")]
        public DateTime? BirthDate { get; init; }

        [Display(Name = "Şehir :")]
        public string? City { get; init; }

        [Display(Name = "Profil Resmi :")]
        public IFormFile? Picture { get; init; }

        [Display(Name = "Cinsiyet :")]
        public byte? Gender { get; init; }

    }
}
