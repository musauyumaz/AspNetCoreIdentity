namespace AspNetCoreIdentityApp.MVC.Models.DTOs
{
    public record EditUserDTO(string Id, string UserName, string Email, string PhoneNumber, DateTime BirthDate, string City, string Picture, byte Gender);
}
