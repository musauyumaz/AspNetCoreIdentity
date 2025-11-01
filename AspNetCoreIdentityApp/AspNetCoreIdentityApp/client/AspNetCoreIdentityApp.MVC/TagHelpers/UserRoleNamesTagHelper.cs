using AspNetCoreIdentityApp.MVC.Areas.Admin.Models.ViewModels;
using AspNetCoreIdentityApp.MVC.Services.Abstractions;
using AspNetCoreIdentityApp.MVC.Services.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace AspNetCoreIdentityApp.MVC.TagHelpers
{
    public class UserRoleNamesTagHelper(IHttpClientService _httpClientService) : TagHelper
    {
        public string UserId { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var result = await _httpClientService.GetAsync<ApiResult<Dictionary<string, AssignRoleToUserViewModel>>>(new(Controller: "Roles", Action: "GetUserRoles"), UserId);

            StringBuilder stringBuilder = new();

            if (result.IsSucceed && result.Data is not null)
            {
                foreach (var role in result.Data)
                {
                    if (role.Value.Exists)
                    {
                        stringBuilder.Append($"<span class='badge text-bg-secondary mx-1'>{role.Value.Name.ToLower()}</span>");
                    }
                }
            }

            output.Content.SetHtmlContent(stringBuilder.ToString());
        }
    }
}
