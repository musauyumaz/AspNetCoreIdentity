using AspNetCoreIdentityApp.Application.Commons.Results;
using AspNetCoreIdentityApp.Application.Features.Users.Commands.ChangePassword;
using AspNetCoreIdentityApp.Application.Features.Users.Commands.ForgetPassword;
using AspNetCoreIdentityApp.Application.Features.Users.Commands.ResetPassword;
using AspNetCoreIdentityApp.Application.Features.Users.DTOs;
using AspNetCoreIdentityApp.Application.Features.Users.Queries.GetAll;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreIdentityApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IMediator _mediator) : ControllerBase
    {
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] UserGetAllQueryRequest getAllUsersQueryRequest)
        {
            Result<List<UserDTO>> data = await _mediator.Send(getAllUsersQueryRequest);
            return StatusCode((int)data.StatusCode, data);
        }

        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword([FromBody] UserForgetPasswordUserCommandRequest forgetPasswordAuthCommandRequest)
        {
            Result<string> data = await _mediator.Send(forgetPasswordAuthCommandRequest);
            return StatusCode((int)data.StatusCode, data);
        }
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] UserResetPasswordCommandRequest resetPasswordAuthCommandRequest)
        {
            Result<string> data = await _mediator.Send(resetPasswordAuthCommandRequest);
            return StatusCode((int)data.StatusCode, data);
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] UserChangePasswordCommandRequest userChangePasswordCommandRequest)
        {
            Result<string> data = await _mediator.Send(userChangePasswordCommandRequest);
            return StatusCode((int)data.StatusCode, data);
        }
    }
}
