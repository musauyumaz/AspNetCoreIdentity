using AspNetCoreIdentityApp.Application.Commons.Results;
using AspNetCoreIdentityApp.Application.Features.Auths.Commands.SignUp;
using AspNetCoreIdentityApp.Application.Features.Auths.DTOs;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreIdentityApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController(IMediator _mediator) : ControllerBase
    {
        public async Task<IActionResult> Login()
        {
            return Ok("Merhaba Dünya");
        }
        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody]SignUpCommandRequest signUpCommandRequest)
        {
            Result<UserDTO> data = await _mediator.Send(signUpCommandRequest);
            return StatusCode((int)data.StatusCode,data);
        }
    }
}
