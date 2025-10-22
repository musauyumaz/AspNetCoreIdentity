using AspNetCoreIdentityApp.MVC.Services.Abstractions;
using AspNetCoreIdentityApp.MVC.Services.Models;
using System.Text;
using System.Text.Json;

namespace AspNetCoreIdentityApp.MVC.Services.Concretes
{
    public class HttpClientService(HttpClient _httpClient, IConfiguration _configuration) : IHttpClientService
    {
        private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

        public async Task<TResponse> DeleteAsync<TResponse>(RequestParameter requestParameter, string id)
        {
            StringBuilder urlBuilder = new StringBuilder();
            urlBuilder.Append(Url(requestParameter));
            urlBuilder.Append(!String.IsNullOrEmpty(id) ? "/" + id : "");
            HttpResponseMessage httpResponseMessage = await _httpClient.DeleteAsync(urlBuilder.ToString());
            TResponse? result = await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(_jsonOptions);
            return result!;
        }

        public async Task<TResponse> GetAsync<TResponse>(RequestParameter requestParameter, string id = null)
        {
            StringBuilder urlBuilder = new StringBuilder();
            urlBuilder.Append(Url(requestParameter));
            urlBuilder.Append(!String.IsNullOrEmpty(id) ? id : "");
            return await _httpClient.GetFromJsonAsync<TResponse>(urlBuilder.ToString(), _jsonOptions);
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(RequestParameter requestParameter, TRequest body)
        {
            string url = Url(requestParameter);
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync<TRequest>(url, body, _jsonOptions);
            TResponse? result = await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(_jsonOptions);
            return result!;
        }

        public async Task<TResponse> PutAsync<TRequest, TResponse>(RequestParameter requestParameter, TRequest body)
        {
            string url = Url(requestParameter);
            HttpResponseMessage httpResponseMessage = await _httpClient.PutAsJsonAsync<TRequest>(url, body, _jsonOptions);
            TResponse? result = await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(_jsonOptions);
            return result!;
        }

        private string Url(RequestParameter requestParameter)
        {
            StringBuilder urlBuilder = new StringBuilder();
            urlBuilder.Append(!String.IsNullOrEmpty(requestParameter.BaseUrl) ? requestParameter.BaseUrl : _configuration["ApiSettings:BaseUrl"]);
            urlBuilder.Append(requestParameter.Controller + "/");
            urlBuilder.Append(!String.IsNullOrEmpty(requestParameter.Action) ? requestParameter.Action : "");
            urlBuilder.Append((!String.IsNullOrEmpty(requestParameter.QueryString) ? "?" + requestParameter.QueryString : ""));
            return !String.IsNullOrEmpty(requestParameter.FullEndpoint) ? requestParameter.FullEndpoint : urlBuilder.ToString();
        }
    }
}
