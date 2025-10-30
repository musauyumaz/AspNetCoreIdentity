using AspNetCoreIdentityApp.Application.Commons.Results;
using AspNetCoreIdentityApp.Application.Features.Roles.Commands.Add;
using AspNetCoreIdentityApp.Application.Features.Roles.Commands.AssignRole;
using AspNetCoreIdentityApp.Application.Features.Roles.Commands.Delete;
using AspNetCoreIdentityApp.Application.Features.Roles.Commands.Update;
using AspNetCoreIdentityApp.Application.Features.Roles.DTOs;
using AspNetCoreIdentityApp.Application.Features.Roles.Queries;
using AspNetCoreIdentityApp.Application.Features.Roles.Queries.Get;
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
        public async Task<IActionResult> GetAll([FromQuery] RoleGetAllQueryRequest roleGetAllQueryRequest)
        {
            Result<List<RoleDTO>> data = await _mediator.Send(roleGetAllQueryRequest);
            return StatusCode((int)data.StatusCode, data);
        }

        [HttpGet("Get/{Id}")]
        public async Task<IActionResult> Get([FromRoute] string Id)
        {
            Result<RoleDTO> data = await _mediator.Send(new RoleGetQueryRequest(Id));
            return StatusCode((int)data.StatusCode, data);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(RoleUpdateCommandRequest roleUpdateCommandRequest)
        {
            Result<string> data = await _mediator.Send(roleUpdateCommandRequest);
            return StatusCode((int)data.StatusCode, data);
        }

        [HttpDelete("Delete/{Id}")]
        public async Task<IActionResult> Delete([FromRoute] string Id)
        {
            Result<string> data = await _mediator.Send(new RoleDeleteCommandRequest(Id));
            return StatusCode((int)data.StatusCode, data);
        }

        [HttpGet("GetUserRoles/{id}")]
        public async Task<IActionResult> GetUserRoles([FromRoute] string id)
        {
            Result<Dictionary<string, GetUserRoleDTO>> data = await _mediator.Send(new GetUserRolesQueryRequest(id));
            return StatusCode((int)data.StatusCode, data);
        }

        [HttpPost("AssignRolesToUser")]
        public async Task<IActionResult> AssignRoleToUser(AssignRoleToUserCommandRequest assignRoleToUserCommandRequest)
        {
            Result<string> data = await _mediator.Send(assignRoleToUserCommandRequest);
            return StatusCode((int)data.StatusCode, data);
        }
    }
}
