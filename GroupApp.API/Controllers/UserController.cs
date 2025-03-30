using GroupApp.API;
using GroupApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IDataRepository _dataRepository;
        public UserController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _dataRepository.GetAll<UserEntity>().ToList();
            if (users == null || !users.Any())
            {
                return NotFound("No users found.");
            }
            return Ok(users);
        }
        [HttpPost]
        public Task<IActionResult> CreateUser([FromBody] UserEntity user)
        {
            if (user == null)
            {
                return Task.FromResult<IActionResult>(BadRequest("User cannot be null."));
            }
            var createdUser = _dataRepository.AddAsync(user);
            return Task.FromResult<IActionResult>(CreatedAtAction(nameof(GetUsers), new { id = createdUser.Id }, createdUser));
        }
    }
}
