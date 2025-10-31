using AspNetCoreIdentityApp.MVC.Models.DTOs;
using AspNetCoreIdentityApp.MVC.Models.ViewModels;
using AspNetCoreIdentityApp.MVC.Services.Abstractions;
using AspNetCoreIdentityApp.MVC.Services.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AspNetCoreIdentityApp.MVC.Controllers
{
    public class HomeController(IHttpClientService _httpClientService) : Controller
    {
        public IActionResult Index() => View();

        public IActionResult Privacy() => View();
        public IActionResult SignUp() => View();
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpRequestDTO signUpRequestDTO)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            ApiResult<object> data = await _httpClientService.PostAsync<SignUpRequestDTO, ApiResult<object>>(new(Controller: "Auths"), signUpRequestDTO);

            if (data.IsSucceed)
            {
                TempData["SuccessMessage"] = "Üyelik Kayıt işlemi başarılı.";

                return RedirectToAction(nameof(HomeController.SignUp));
            }

            ModelState.AddModelError(string.Empty, data.ErrorMessage ?? "");

            return View();
        }

        [HttpGet]
        public IActionResult SignIn() => View();


        [HttpPost]
        public async Task<IActionResult> SignIn(SignInRequestDTO signInRequestDTO, string? returnUrl = null)
        {
            _ = returnUrl ?? Url.Action("Index", "Home");

            ApiResult<UserResponseDTO> data = await _httpClientService.PostAsync<SignInRequestDTO, ApiResult<UserResponseDTO>>(new(Controller: "Auths", Action: "login"), signInRequestDTO);

            if (data.IsSucceed)
            {
                var claims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, data.Data?.Id ?? string.Empty),
                    new(ClaimTypes.Name, data.Data?.UserName ?? string.Empty),
                    new(ClaimTypes.Email, data.Data?.Email ?? string.Empty),
                    new(ClaimTypes.MobilePhone, data.Data?.PhoneNumber ?? string.Empty)
                };

                if (data.Data?.Roles is not null && data.Data.Roles.Any())
                {
                    data.Data.Roles.ForEach(role =>
                    {
                        claims.Add(new(ClaimTypes.Role, role));
                        System.Diagnostics.Debug.WriteLine($"Claim ekleniyor: {role}");
                    });

                }

                System.Diagnostics.Debug.WriteLine($"Claims toplam sayısı: {claims.Count}");
                foreach (var claim in claims)
                {
                    System.Diagnostics.Debug.WriteLine($"Claim: {claim.Type} = {claim.Value}");
                }

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(claimsIdentity);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = signInRequestDTO.RememberMe,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(60)
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

                TempData["SuccessMessage"] = "Giriş işlemi başarılı.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, data.ErrorMessage ?? "");

            return View();
        }

        public IActionResult ForgetPassword() => View();
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordRequestDTO forgetPasswordRequestDTO)
        {
            var data = await _httpClientService.PostAsync<ForgetPasswordRequestDTO, ApiResult<string>>(new(Controller: "Users", Action: "ForgetPassword"), forgetPasswordRequestDTO);

            if (!data.IsSucceed)
            {
                ModelState.AddModelError(string.Empty, data.ErrorMessage ?? "");
                return View();
            }

            TempData["SuccessMessage"] = "Şifre sıfırlama linki email adresinize gönderilmiştir.";
            return RedirectToAction(nameof(ForgetPassword));
        }

        [HttpGet]
        public IActionResult ResetPassword(string userId, string token)
        {
            TempData["userId"] = userId;
            TempData["token"] = token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel request)
        {
            string? userId = TempData["userId"]?.ToString();
            string? token = TempData["token"]?.ToString();

            var result =  await _httpClientService.PostAsync<ResetPasswordRequestDTO, ApiResult<string>>(new(Controller: "Users", Action: "ResetPassword"),new(userId,token, request.Password));

            if (!result.IsSucceed)
                ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "");

            TempData["SuccessMessage"] = result.Data;

            return View();
        }
    }

}
