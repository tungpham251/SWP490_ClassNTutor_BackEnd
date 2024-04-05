using BusinessLogic.Services.Interfaces;
using DataAccess.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [Authorize(Roles = "STAFF")]
        [HttpGet("get-roles")]
        public async Task<IActionResult> GetRoles([FromQuery] RoleRequestDto entity)
        {
            var result = await _roleService.GetRoles(entity).ConfigureAwait(false);
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }

        [Authorize(Roles = "STAFF")]
        [HttpGet("get-role-by-id/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _roleService.GetById(id).ConfigureAwait(false);
            if (result == null) return NotFound(new ApiFormatResponse(StatusCodes.Status404NotFound, false, result));
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }

        [Authorize(Roles = "STAFF")]
        [HttpPost("add-role")]
        public async Task<IActionResult> AddRole([FromForm] RoleDto entity)
        {
            var result = await _roleService.AddRole(entity).ConfigureAwait(false);
            if (!result) return BadRequest(new ApiFormatResponse(StatusCodes.Status400BadRequest, false, result));
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }

        [Authorize(Roles = "STAFF")]
        [HttpPut("update-role")]
        public async Task<IActionResult> UpdateRole([FromForm] RoleDto entity)
        {
            var result = await _roleService.UpdateRole(entity).ConfigureAwait(false);
            if (!result) return BadRequest(new ApiFormatResponse(StatusCodes.Status400BadRequest, false, result));
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }

        [Authorize(Roles = "STAFF")]
        [HttpDelete("delete-role/{id}")]
        public async Task<IActionResult> DeleteRole([FromQuery] int id)
        {
            var result = await _roleService.DeleteRole(id).ConfigureAwait(false);
            if (!result) return BadRequest(new ApiFormatResponse(StatusCodes.Status400BadRequest, false, result));
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }
    }
}
