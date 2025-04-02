using AutoMapper;
using FluentValidation;
using GroupApp.Core.Concrete;
using GroupApp.Core.Services;
using GroupApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : GenericController<UserEntity, UserDTO>
{
    private readonly IUserService _userService;

    public UserController(IUserService userService, IValidator<UserDTO> validator, IMapper mapper)
        : base(userService, validator, mapper)
    {
        _userService = userService;
    }
    [HttpGet("/api/list-users")]
    public async Task<IActionResult> ListUsers()
    {
        var result = await _userService.ListUsers();
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }
}

}