using AspNetCoreIdentityApp.MVC.Models;
using AspNetCoreIdentityApp.MVC.Services.Abstractions;
using AspNetCoreIdentityApp.MVC.Services.Models;
using Microsoft.AspNetCore.Mvc;

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
    }
}
