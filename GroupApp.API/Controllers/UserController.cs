using System.Threading.Tasks;
using GroupApp.API;
using GroupApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GroupApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        private IUserService _userService = userService;
        [HttpGet("/api/users")]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _userService.GetAllUsersAsync();

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }
        [HttpPost("/api/create/user")]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO dto)
        {
            var result = await _userService.CreateUserAsync(dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
        [HttpPut("/api/update/user{id}")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDTO dto, int id)
        {
            var result = await _userService.UpdateUserAsync(id, dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
        [HttpDelete("/api/delete/user{id}")]
        public async Task<IActionResult> Deleteuser(int id)
        {
            var result = await _userService.DeleteUserAsync(id);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }
        [HttpGet("/api/users/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var result = await _userService.GetUserByIdAsync(id);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }
    }
}
