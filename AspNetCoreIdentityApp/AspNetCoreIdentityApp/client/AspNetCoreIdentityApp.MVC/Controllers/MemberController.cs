using AspNetCoreIdentityApp.MVC.Models.DTOs;
using AspNetCoreIdentityApp.MVC.Models.ViewModels;
using AspNetCoreIdentityApp.MVC.Services.Abstractions;
using AspNetCoreIdentityApp.MVC.Services.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AspNetCoreIdentityApp.MVC.Controllers
{
    [Authorize]
    public class MemberController(IHttpClientService _httpClientService, IFileProvider _fileProvider) : Controller
    {
        public async Task SignOut() => await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        public IActionResult Index() => View();
        public IActionResult PasswordChange() => View();
        [HttpPost]
        public async Task<IActionResult> PasswordChange(PasswordChangeViewModel passwordChangeRequestDTO)
        {
            if (!ModelState.IsValid) return View();

            ApiResult<string> result = await _httpClientService.PostAsync<PasswordChangeRequestDTO, ApiResult<string>>(new(Controller: "Users", Action: "ChangePassword"), new PasswordChangeRequestDTO(User.Identity?.Name ?? string.Empty, passwordChangeRequestDTO.OldPassword, passwordChangeRequestDTO.NewPassword));

            if (!result.IsSucceed)
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "");
                return View();
            }

            return RedirectToAction("Index", "Member");
        }

        public async Task<IActionResult> EditUser()
        {

            ApiResult<UserResponseDTO> result = await _httpClientService.GetAsync<ApiResult<UserResponseDTO>>(new(Controller: "Users", Action: "FindById"), User.FindFirstValue(ClaimTypes.NameIdentifier));

            EditUserViewModel editUserViewModel = new() { 
                UserName = result.Data.UserName,
                BirthDate = result.Data.BirthDate,
                City = result.Data.City,
                Email = result.Data.Email,
                Gender = result.Data.Gender,
                PhoneNumber = result.Data.PhoneNumber };

            ViewBag.genderList = new SelectList(new[]
            {
                new { Value = "1", Text = "Kadın" },
                new { Value = "2", Text = "Erkek" }
            }, "Value", "Text");

            return View(editUserViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel editUserViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.genderList = new SelectList(new[]
                {
                    new { Value = "1", Text = "Kadın" },
                    new { Value = "2", Text = "Erkek" }
                }, "Value", "Text");
                return View(editUserViewModel);
            }

            if (editUserViewModel.Picture is not null && editUserViewModel.Picture.Length > 0)
            {
                string userPicturesPath = Path.Combine("wwwroot", "UserPictures");

                if (!Directory.Exists(userPicturesPath))
                    Directory.CreateDirectory(userPicturesPath);

                string randomFileName = $"{Guid.NewGuid()}{Path.GetExtension(editUserViewModel.Picture.FileName)}";
                string newPicturePath = Path.Combine(userPicturesPath, randomFileName);

                using var stream = new FileStream(newPicturePath, FileMode.Create);
                await editUserViewModel.Picture.CopyToAsync(stream);
            }

            var updatedUserResult = await _httpClientService.PutAsync<EditUserDTO, ApiResult<string>>(new(Controller: "Users", Action: "Update"), new EditUserDTO(
                User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty,
                editUserViewModel.UserName,
                editUserViewModel.Email,
                editUserViewModel.PhoneNumber,
                editUserViewModel.BirthDate ?? DateTime.MinValue,
                editUserViewModel.City ?? string.Empty,
                editUserViewModel.Picture is not null ? editUserViewModel.Picture.FileName : string.Empty,
                editUserViewModel.Gender ?? 0
                ));

            if (!updatedUserResult.IsSucceed)
            {
                ModelState.AddModelError(string.Empty, updatedUserResult.ErrorMessage ?? "");
                return View();
            }

            TempData["SuccessMessage"] = "Kullanıcı bilgileri başarıyla güncellendi.";
            return View();
        }
    }
}
