using AspNetCoreIdentityApp.Application.Abstractions.Services;
using AspNetCoreIdentityApp.Application.Commons.Results;
using AspNetCoreIdentityApp.Application.Features.Auths.Commands.ForgetPassword;
using AspNetCoreIdentityApp.Application.Features.Auths.Commands.ResetPassword;
using AspNetCoreIdentityApp.Application.Features.Auths.Commands.SignIn;
using AspNetCoreIdentityApp.Application.Features.Auths.Commands.SignUp;
using AspNetCoreIdentityApp.Application.Features.Auths.DTOs;
using AspNetCoreIdentityApp.Application.Features.Auths.Queries.GetAll;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AspNetCoreIdentityApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController(IMediator _mediator, IEmailService _emailService) : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<IActionResult> SignIn([FromBody] SignInAuthCommandRequest signInAuthCommandRequest)
        {
            Result<UserDTO> data = await _mediator.Send(signInAuthCommandRequest);
            return StatusCode((int)data.StatusCode, data);
        }
        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] SignUpAuthCommandRequest signUpCommandRequest)
        {
            Result<UserDTO> data = await _mediator.Send(signUpCommandRequest);
            return StatusCode((int)data.StatusCode, data);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllUsersQueryRequest getAllUsersQueryRequest)
        {
            Result<List<UserDTO>> data = await _mediator.Send(getAllUsersQueryRequest);
            return StatusCode((int)data.StatusCode, data);
        }

        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordAuthCommandRequest forgetPasswordAuthCommandRequest)
        {
            Result<string> data = await _mediator.Send(forgetPasswordAuthCommandRequest);
            return StatusCode((int)data.StatusCode, data);
        }
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordAuthCommandRequest resetPasswordAuthCommandRequest)
        {
            Result<string> data = await _mediator.Send(resetPasswordAuthCommandRequest);
            return StatusCode((int)data.StatusCode, data);
        }
    }
}
