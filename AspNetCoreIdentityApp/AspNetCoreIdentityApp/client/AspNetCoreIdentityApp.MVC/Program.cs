using AspNetCoreIdentityApp.MVC.Models;
using AspNetCoreIdentityApp.MVC.Requirements;
using AspNetCoreIdentityApp.MVC.Services.Abstractions;
using AspNetCoreIdentityApp.MVC.Services.Concretes;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));
builder.Services.AddHttpClient<IHttpClientService, HttpClientService>((sp, client) =>
{
    var options = sp.GetRequiredService<IOptions<ApiSettings>>().Value;

    client.BaseAddress = new Uri(options.BaseUrl);
    client.Timeout = TimeSpan.FromSeconds(options.TimeoutSeconds);
    client.DefaultRequestHeaders.Clear();
    client.DefaultRequestHeaders.Add("Accept", options.DefaultAcceptHeader);
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "AppCookie";
        options.LoginPath = new("/Home/SignIn");
        options.LogoutPath = new("/Member/SignOut");
        options.AccessDeniedPath = new("/Member/AccessDenied");
        options.ExpireTimeSpan = TimeSpan.FromDays(60);
        options.SlidingExpiration = true;
    });

builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
builder.Services.AddScoped<IAuthorizationHandler, ExchangeExpireRequirementHandler>();
builder.Services.AddScoped<IAuthorizationHandler, ViolenceRequirementHandler>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AnkaraPolicy", policy =>
    {
        policy.RequireClaim("city", "Ankara");
        //policy.RequireClaim("city", "Eskişehir", "Ankara");
    });

    options.AddPolicy("ExchangePolicy", policy =>
    {
        policy.AddRequirements(new ExchangeExpireRequirement());
    });

    options.AddPolicy("ViolencePolicy", policy =>
    {
        policy.AddRequirements(new ViolenceRequirement() { ThresoldAge = 18});
    });

    options.AddPolicy("OrderPermissionReadOrDelete", policy =>
    {
        policy.RequireClaim("Permission", "Order.Read");
        policy.RequireClaim("Permission", "Order.Delete");
        policy.RequireClaim("Permission", "Stock.Delete");
    });
});


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
