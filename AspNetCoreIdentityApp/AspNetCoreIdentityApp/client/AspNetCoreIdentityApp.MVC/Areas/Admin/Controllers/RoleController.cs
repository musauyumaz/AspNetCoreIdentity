using AspNetCoreIdentityApp.MVC.Areas.Admin.Models.DTOs;
using AspNetCoreIdentityApp.MVC.Areas.Admin.Models.ViewModels;
using AspNetCoreIdentityApp.MVC.Services.Abstractions;
using AspNetCoreIdentityApp.MVC.Services.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AspNetCoreIdentityApp.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController(IHttpClientService _httpClientService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            ApiResult<List<RoleViewModel>>? data = await _httpClientService.GetAsync<ApiResult<List<RoleViewModel>>>(new(Controller: "Roles", Action: "GetAll"));
            return View(data.Data);
        }

        public IActionResult Add() => View();
        [HttpPost]
        public async Task<IActionResult> Add(AddRoleDTO addRoleDTO) 
        {
            ApiResult<string>? result = await _httpClientService.PostAsync<AddRoleDTO, ApiResult<string>>(new(Controller: "Roles", Action: "Add"), addRoleDTO);

            if (!result.IsSucceed)
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "");
                return View();
            }
            

            TempData["SuccessMessage"] = "Rol ekleme işlemi başarılı.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UpdateAsync(string id) 
        {
            ApiResult<UpdateRoleDTO>? result = await _httpClientService.GetAsync<ApiResult<UpdateRoleDTO>>(new(Controller: "Roles", Action: "Get"),id);
            return View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateRoleDTO updateRoleDTO)
        {
            ApiResult<string>? result = await _httpClientService.PutAsync<UpdateRoleDTO, ApiResult<string>>(new(Controller: "Roles", Action: "Update"), updateRoleDTO);

            if (!result.IsSucceed)
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "");
                return View();
            }


            TempData["SuccessMessage"] = "Rol güncelleme işlemi başarılı.";
            return RedirectToAction(nameof(Index));
        }
    }
}
