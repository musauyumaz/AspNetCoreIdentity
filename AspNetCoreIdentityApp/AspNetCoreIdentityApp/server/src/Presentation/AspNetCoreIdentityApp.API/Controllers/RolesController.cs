using AspNetCoreIdentityApp.Application.Commons.Results;
using AspNetCoreIdentityApp.Application.Features.Roles.Commands.Add;
using AspNetCoreIdentityApp.Application.Features.Roles.Queries.GetAll;
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

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery]RoleGetAllQueryRequest roleGetAllQueryRequest)
        {
            Result<List<Application.Features.Roles.DTOs.RoleDTO>> data = await _mediator.Send(roleGetAllQueryRequest);
            return StatusCode((int)data.StatusCode, data);
        }
    }
}
