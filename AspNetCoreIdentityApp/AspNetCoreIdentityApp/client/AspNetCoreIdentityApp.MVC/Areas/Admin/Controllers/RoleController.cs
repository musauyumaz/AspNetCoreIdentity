using AspNetCoreIdentityApp.MVC.Areas.Admin.Models.DTOs;
using AspNetCoreIdentityApp.MVC.Areas.Admin.Models.ViewModels;
using AspNetCoreIdentityApp.MVC.Services.Abstractions;
using AspNetCoreIdentityApp.MVC.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AspNetCoreIdentityApp.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class RoleController(IHttpClientService _httpClientService) : Controller
    {
        [Authorize(Roles ="admin,role-action")]
        public async Task<IActionResult> Index()
        {
            ApiResult<List<RoleViewModel>>? data = await _httpClientService.GetAsync<ApiResult<List<RoleViewModel>>>(new(Controller: "Roles", Action: "GetAll"));
            return View(data.Data);
        }

        [Authorize(Roles = "role-action")]
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
        [Authorize(Roles = "role-action")]
        public async Task<IActionResult> UpdateAsync(string id)
        {
            ApiResult<UpdateRoleDTO>? result = await _httpClientService.GetAsync<ApiResult<UpdateRoleDTO>>(new(Controller: "Roles", Action: "Get"), id);
            return View(result.Data);
        }

        [Authorize(Roles = "role-action")]
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

        [Authorize(Roles = "role-action")]
        public async Task<IActionResult> Delete(string id)
        {
            ApiResult<string>? result = await _httpClientService.DeleteAsync<ApiResult<string>>(new(Controller: "Roles", Action: "Delete"), id);
            if (!result.IsSucceed)
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "");
                return RedirectToAction(nameof(Index));
            }
            TempData["SuccessMessage"] = "Rol silme işlemi başarılı.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AssignRoleToUser(string id)
        {
            var result = await _httpClientService.GetAsync<ApiResult<Dictionary<string, AssignRoleToUserViewModel>>>(new(Controller: "Roles", Action: "GetUserRoles"), id);
            ViewBag.userId = id;
            return View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRoleToUser(AssignRoleDTO request)
        {
            ApiResult<string>? result = await _httpClientService.PostAsync<AssignRoleDTO, ApiResult<string>>(
      new(Controller: "Roles", Action: "AssignRolesToUser"), request);

            if (!result.IsSucceed)
            {
                ModelState.AddModelError("", result.ErrorMessage ?? "");
                return View();
            }

            return RedirectToAction("UserList", "Home");
        }
    }
}
