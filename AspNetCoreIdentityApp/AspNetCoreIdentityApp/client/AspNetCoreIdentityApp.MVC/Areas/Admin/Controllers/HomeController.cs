using AspNetCoreIdentityApp.MVC.Areas.Admin.Models;
using AspNetCoreIdentityApp.MVC.Services.Abstractions;
using AspNetCoreIdentityApp.MVC.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreIdentityApp.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController(IHttpClientService _httpClientService) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UserList()
        {
            ApiResult<List<UserViewModel>> datas = await _httpClientService.GetAsync<ApiResult<List<UserViewModel>>>(new(Controller: "Auths", Action: "GetAll"));
            return View(datas.Data);
        }
    }
}
