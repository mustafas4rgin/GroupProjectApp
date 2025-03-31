using GroupApp.Core.Concrete;
using GroupApp.Core.Helpers;
using GroupApp.Core.Results;
using GroupApp.Data;

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
        var users = await repository.GetAllAsync();
        var user = users.Where(x => x.Email == dto.Email && HashingHelper.VerifyPasswordHash(dto.Password, x.PasswordHash, x.PasswordSalt)).FirstOrDefault();
        if (user == null)
        {
            return new ServiceResult<UserEntity>
            {
                Message = "Invalid email or password.",
                Success = false,
            };
        }
        return new ServiceResult<UserEntity>
        {
            Message = "Login successful.",
            Success = true,
            Data = user
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
}