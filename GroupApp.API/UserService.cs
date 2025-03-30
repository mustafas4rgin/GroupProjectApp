using GroupApp.API.Results;
using GroupApp.Data;

namespace GroupApp.API;

public class UserService(IDataRepository dataRepository) : IUserService
{
    private IDataRepository _dataRepository = dataRepository;

    public async Task<IServiceResult<UserEntity>> GetUserByIdAsync(int id)
    {
        var user = await _dataRepository.GetByIdAsync<UserEntity>(id);
        if (user == null)
        {
            return new ServiceResult<UserEntity>
            {
                Success = false,
                Message = "User not found"
            };
        }

        return new ServiceResult<UserEntity>
        {
            Success = true,
            Data = user
        };
    }
    public async Task<IServiceResult> CreateUserAsync(UserDTO dto)
    {
        var user = MappingHelper.MapToUserEntity(dto);
        await _dataRepository.AddAsync(user);
        return new ServiceResult
        {
            Success = true,
            Message = "User created successfully"
        };
    }
}
