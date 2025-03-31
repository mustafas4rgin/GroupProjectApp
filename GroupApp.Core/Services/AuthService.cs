using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GroupApp.Core.Concrete;
using GroupApp.Core.Helpers;
using GroupApp.Core.Results;
using GroupApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace GroupApp.Core.Services;


public class AuthService : Service<UserEntity>, IAuthService
{
    private readonly IRepository<UserEntity> repository;
    public AuthService(IRepository<UserEntity> repository) : base(repository)
    {
        this.repository = repository;
    }
    public async Task<IServiceResult<UserEntity>> LoginAsync(LoginDTO dto)
{
    var user = await repository
        .GetAllAsync().Include(x => x.Role)
        .FirstOrDefaultAsync(x => x.Email == dto.Email);

    if (user == null || !HashingHelper.VerifyPasswordHash(dto.Password, user.PasswordHash, user.PasswordSalt))
    {
        return new ServiceResult<UserEntity>
        {
            Message = "Invalid email or password.",
            Success = false,
        };
    }

    var token = GenerateJwtToken(user);

    return new ServiceResult<UserEntity>
    {
        Data = user,
        Success = true,
        Message = token,
    };
}

    public async Task<IServiceResult> RegisterAsync(UserDTO dto)
    {
        //TODO: Validation
        var user = MappingHelper.MapToUserEntity(dto);

        await repository.AddAsync(user);

        return new ServiceResult
        {
            Message = "User registered successfully.",
            Success = true,
        };
    }
    private static string GenerateJwtToken(UserEntity user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("HayatimdakiEnGuvenliAnahtarBuOlsaGerek230723")); 
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.FirstName),
            new Claim(ClaimTypes.Role, user.Role.Name) 
        };

        var token = new JwtSecurityToken(
            issuer: "groupapp.com",
            audience: "groupapp.com",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}