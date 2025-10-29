using AspNetCoreIdentityApp.Application.Commons.Results;
using AspNetCoreIdentityApp.Application.Features.Roles.Commands.Add;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreIdentityApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController(IMediator _mediator) : ControllerBase
    {
        [HttpPost("Add")]
        public async Task<IActionResult> Add(RoleAddCommandRequest roleAddCommandRequest)
        {
            Result<string> data = await _mediator.Send(roleAddCommandRequest);
            return StatusCode((int)data.StatusCode, data);
        }
    }
}
