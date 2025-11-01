using AspNetCoreIdentityApp.MVC.Areas.Admin.Models.ViewModels;
using AspNetCoreIdentityApp.MVC.Services.Abstractions;
using AspNetCoreIdentityApp.MVC.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreIdentityApp.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class HomeController(IHttpClientService _httpClientService) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UserList()
        {
            ApiResult<List<UserViewModel>> datas = await _httpClientService.GetAsync<ApiResult<List<UserViewModel>>>(new(Controller: "Users", Action: "GetAll"));
            return View(datas.Data);
        }
    }
}
