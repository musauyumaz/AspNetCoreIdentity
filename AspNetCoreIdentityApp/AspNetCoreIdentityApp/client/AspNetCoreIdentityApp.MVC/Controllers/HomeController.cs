using AspNetCoreIdentityApp.MVC.Models;
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
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel signUpViewModel)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            ApiResult<object> data = await _httpClientService.PostAsync<SignUpViewModel, ApiResult<object>>(new(Controller: "Auths"), signUpViewModel);

            if (data.IsSucceed)
            {
                TempData["SuccessMessage"] = "Üyelik Kayıt işlemi başarılı.";

                return RedirectToAction(nameof(HomeController.SignUp));
            }

            ModelState.AddModelError(string.Empty, data.ErrorMessage ?? "");

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SignIn()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel signInViewModel, string? returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Action("Index", "Home");

            ApiResult<UserDTO> data = await _httpClientService.PostAsync<SignInViewModel, ApiResult<UserDTO>>(new(Controller: "Auths", Action: "login"), signInViewModel);

            if (data.IsSucceed)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, data.Data.Id ?? string.Empty),
                    new Claim(ClaimTypes.Name, data.Data.UserName ?? string.Empty),
                    new Claim(ClaimTypes.Email, data.Data.Email ?? string.Empty)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(claimsIdentity);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = signInViewModel.RememberMe,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(60)
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

                TempData["SuccessMessage"] = "Giriş işlemi başarılı.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, data.ErrorMessage ?? "");

            return View();
        }

        public async Task<IActionResult> ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel resetPasswordViewModel)
        {
            var data = await _httpClientService.PostAsync<ForgetPasswordViewModel, ApiResult<string>>(new(Controller: "Auths", Action: "ForgetPassword"), resetPasswordViewModel);

            if (!data.IsSucceed)
            {
                ModelState.AddModelError(string.Empty, data.ErrorMessage ?? "");
                return View();
            }

            TempData["SuccessMessage"] = "Şifre sıfırlama linki email adresinize gönderilmiştir.";
            return RedirectToAction(nameof(ForgetPassword));
        }
    }
}
