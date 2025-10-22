using AspNetCoreIdentityApp.MVC.Models;
using AspNetCoreIdentityApp.MVC.Services.Abstractions;
using AspNetCoreIdentityApp.MVC.Services.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
                TempData["SuccessMessage"] = "Giriş işlemi başarılı.";

                return RedirectToAction(returnUrl);
            }

            ModelState.AddModelError(string.Empty, data.ErrorMessage ?? "");

            return View();
        }
    }
}
