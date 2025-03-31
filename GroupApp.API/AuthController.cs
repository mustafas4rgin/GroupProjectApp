using FluentValidation;
using GroupApp.Core.Concrete;
using GroupApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IValidator<LoginDTO> validator;
        public AuthController(IAuthService authService, IValidator<LoginDTO> validator)
        {
            this.validator = validator;
            this.authService = authService;
        }
        [HttpPost("/api/register")]
        public async Task<IActionResult> Register([FromBody]UserDTO dto)
        {
            //validation
            var result = await authService.RegisterAsync(dto);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
        [HttpPost("/api/login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            //validation
            var validationResult = await validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await authService.LoginAsync(dto);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
    }
}
