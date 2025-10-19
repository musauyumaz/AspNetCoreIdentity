using System.Net;

namespace AspNetCoreIdentityApp.MVC.Services.Models
{
    public readonly record struct ApiResult<T>(
     bool IsSucceed,
     T? Data = default,
     string? ErrorMessage = null,
     HttpStatusCode? StatusCode = null);
}
