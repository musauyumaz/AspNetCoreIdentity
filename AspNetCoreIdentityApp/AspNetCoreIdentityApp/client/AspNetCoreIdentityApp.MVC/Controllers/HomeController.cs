using AspNetCoreIdentityApp.MVC.Models;
using AspNetCoreIdentityApp.MVC.Services.Abstractions;
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
            var data = await _httpClientService.SendAsync<SignUpViewModel>(new("auths"));
            return View();
        }
    }
}
