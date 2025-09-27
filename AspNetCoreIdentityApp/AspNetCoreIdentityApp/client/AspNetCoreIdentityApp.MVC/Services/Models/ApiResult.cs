using System.Net;

namespace AspNetCoreIdentityApp.MVC.Services.Models
{
    public record ApiResult<T>(
        bool Success,
        T? Data,
        string? ErrorMessage = null,
        HttpStatusCode? StatusCode = null);
}
