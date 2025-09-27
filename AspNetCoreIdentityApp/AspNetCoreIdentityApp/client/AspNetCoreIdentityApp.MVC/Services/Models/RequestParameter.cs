using System.Net.Http.Headers;

namespace AspNetCoreIdentityApp.MVC.Services.Models;

public sealed record RequestParameter(
 string Controller,
 string? Action = null,
 string? QueryString = null,
 HttpHeaders? HttpHeaders = null,
 string? BaseUrl = null,
 Uri? FullEndpoint = null,
 HttpMethod? Method = null,
 HttpContent? Content = null,
 CancellationToken CancellationToken = default
);
