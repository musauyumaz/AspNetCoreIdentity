using AspNetCoreIdentityApp.MVC.Models.DTOs;
using AspNetCoreIdentityApp.MVC.Models.ViewModels;
using AspNetCoreIdentityApp.MVC.Services.Abstractions;
using AspNetCoreIdentityApp.MVC.Services.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreIdentityApp.MVC.Controllers
{
    [Authorize]
    public class MemberController(IHttpClientService _httpClientService) : Controller
    {
        public async Task SignOut() => await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        public IActionResult Index() => View();
        public IActionResult PasswordChange() => View();
        public async Task<IActionResult> PasswordChange(PasswordChangeViewModel passwordChangeRequestDTO) 
        {
            if (!ModelState.IsValid) return View();

            ApiResult<string> result = await _httpClientService.PostAsync<PasswordChangeRequestDTO, ApiResult<string>>(new(Controller: "Users", Action: "ChangePassword"), new PasswordChangeRequestDTO(User.Identity?.Name ?? string.Empty, passwordChangeRequestDTO.OldPassword, passwordChangeRequestDTO.NewPassword));

            if (!result.IsSucceed)
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "");
                return View();
            }

            return RedirectToAction("Index","Member");
        }
    }
}
