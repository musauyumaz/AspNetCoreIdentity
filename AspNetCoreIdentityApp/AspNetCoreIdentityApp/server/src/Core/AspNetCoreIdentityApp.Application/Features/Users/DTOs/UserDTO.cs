namespace AspNetCoreIdentityApp.Application.Features.Users.DTOs
{
    public record UserDTO(string Id, string UserName, string Email, string PhoneNumber, string City, DateTime BirthDate, byte Gender, string Picture, List<string> Roles);
}
