using AspNetCoreIdentityApp.MVC.Services.Abstractions;
using AspNetCoreIdentityApp.MVC.Services.Models;
using System.Net.Http;
using System.Text.Json;

public sealed class HttpClientService : IHttpClientService
{
    private readonly HttpClient _httpClient;

    public HttpClientService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<HttpResponseMessage> SendAsync(RequestParameter requestParameter)
    {
        if (requestParameter is null)
            throw new ArgumentNullException(nameof(requestParameter));

        HttpRequestMessage? request = BuildHttpRequest(requestParameter);
        return await _httpClient.SendAsync(request, requestParameter.CancellationToken);
    }

    public async Task<ApiResult<T>> SendAsync<T>(RequestParameter requestParameter, JsonSerializerOptions? options = null)
    {
        HttpResponseMessage? response = await SendAsync(requestParameter);
        response.EnsureSuccessStatusCode();

        await using Stream? stream = await response.Content.ReadAsStreamAsync(requestParameter.CancellationToken);
        ApiResult<T>? apiResult = await JsonSerializer.DeserializeAsync<ApiResult<T>>(stream, options, requestParameter.CancellationToken);

        if (apiResult is null)
            throw new InvalidOperationException("API returned empty response");

        return apiResult;
    }

    private static HttpRequestMessage BuildHttpRequest(RequestParameter requestParameter)
    {
        var uri = requestParameter.FullEndpoint ?? new UriBuilder(requestParameter.BaseUrl)
        {
            Path = $"{requestParameter.Controller}/{requestParameter.Action}".TrimEnd('/'),
            Query = string.IsNullOrWhiteSpace(requestParameter.QueryString)
                ? string.Empty
                : requestParameter.QueryString.TrimStart('?')
        }.Uri;

        var request = new HttpRequestMessage(requestParameter.Method ?? HttpMethod.Get, uri);

        if (requestParameter.HttpHeaders is not null)
        {
            foreach (var header in requestParameter.HttpHeaders)
                request.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }

        if (requestParameter.Content is not null)
            request.Content = requestParameter.Content;

        return request;
    }
}
