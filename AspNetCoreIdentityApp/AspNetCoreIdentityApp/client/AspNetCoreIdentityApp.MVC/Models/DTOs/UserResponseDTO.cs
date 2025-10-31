namespace AspNetCoreIdentityApp.MVC.Models.DTOs;

public record UserResponseDTO(string Id, string UserName, string Email, string PhoneNumber, string City, DateTime BirthDate, byte Gender, string Picture, List<string> Roles);
