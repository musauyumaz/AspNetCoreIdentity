using AspNetCoreIdentityApp.MVC.Services.Models;
using System.Text.Json;

namespace AspNetCoreIdentityApp.MVC.Services.Abstractions
{
    public interface IHttpClientService
    {
        Task<HttpResponseMessage> SendAsync(RequestParameter requestParameter);
        Task<ApiResult<T?>> SendAsync<T>(RequestParameter requestParameter, JsonSerializerOptions? options = null);
    }

}
