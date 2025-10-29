using AspNetCoreIdentityApp.Application.Commons.Results;
using AspNetCoreIdentityApp.Application.Features.Auths.Commands.SignIn;
using AspNetCoreIdentityApp.Application.Features.Auths.Commands.SignUp;
using AspNetCoreIdentityApp.Application.Features.Users.DTOs;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreIdentityApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController(IMediator _mediator) : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<IActionResult> SignIn([FromBody] AuthSignInCommandRequest signInAuthCommandRequest)
        {
            Result<UserDTO> data = await _mediator.Send(signInAuthCommandRequest);
            return StatusCode((int)data.StatusCode, data);
        }
        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] AuthSignUpCommandRequest signUpCommandRequest)
        {
            Result<UserDTO> data = await _mediator.Send(signUpCommandRequest);
            return StatusCode((int)data.StatusCode, data);
        }
    }
}
