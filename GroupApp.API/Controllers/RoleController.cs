using System.Threading.Tasks;
using GroupApp.API;
using GroupApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController(IRoleService roleService) : ControllerBase
    {
        private IRoleService _roleService = roleService;
        [HttpGet("/api/update/role/{id}")]
        public async Task<IActionResult> UpdateRole([FromBody] RoleDTO dto, int id)
        {
            var result = await _roleService.UpdateRoleAsync(dto, id);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Message);
        }
        [HttpGet("/api/roles")]
        public async Task<IActionResult> GetRoles()
        {
            var result = await _roleService.GetAllRolesAsync();
            if (!result.Success)
            {
                return NotFound(result.Message);
            }
            return Ok(new { Data = result.Data, Message = result.Message });
        }
        [HttpPost("/api/create/role")]
        public async Task<IActionResult> CreateRole([FromBody] RoleDTO dto)
        {
            var result = await _roleService.CreateRoleAsync(dto);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Message);
        }
        [HttpDelete("/api/delete/role/{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var result = await _roleService.DeleteRoleAsync(id);
            if(!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Message);
        }
        [HttpGet("/api/roles/{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var result = await _roleService.GetRoleByIdAsync(id);
            if(!result.Success)
            {
                return NotFound(result.Message);
            }
            return Ok(new { Data = result.Data, Message = result.Message });
        }
    }

}
