using System.Net;

namespace AspNetCoreIdentityApp.MVC.Services.Models
{
    public record ApiResult<T>(
     bool IsSucceed,
     T? Data = default,
     string? ErrorMessage = null,
     HttpStatusCode? StatusCode = null);
}
