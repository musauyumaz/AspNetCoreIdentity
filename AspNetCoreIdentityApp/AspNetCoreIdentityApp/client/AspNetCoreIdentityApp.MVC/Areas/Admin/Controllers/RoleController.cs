using AspNetCoreIdentityApp.MVC.Areas.Admin.Models.DTOs;
using AspNetCoreIdentityApp.MVC.Services.Abstractions;
using AspNetCoreIdentityApp.MVC.Services.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AspNetCoreIdentityApp.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController(IHttpClientService _httpClientService) : Controller
    {
        public IActionResult Index() => View();

        public IActionResult Add() => View();
        [HttpPost]
        public async Task<IActionResult> Add(AddRoleDTO addRoleDTO) 
        {
            var result = await _httpClientService.PostAsync<AddRoleDTO, ApiResult<string>>(new(Controller: "Roles", Action: "Add"), addRoleDTO);

            if (!result.IsSucceed)
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "");
                return View();
            }
            

            TempData["SuccessMessage"] = "Rol ekleme işlemi başarılı.";
            return RedirectToAction(nameof(Index));
        }
    }
}
