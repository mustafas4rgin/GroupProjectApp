using GroupApp.API.Results;
using GroupApp.Data;

namespace GroupApp.API;

public interface IUserService
{
    Task<IServiceResult<UserEntity>> GetUserByIdAsync(int id);
    Task<IServiceResult> CreateUserAsync(UserDTO userDto);
    Task<IServiceResult> UpdateUserAsync(int id, UserDTO userDto);
    Task<IServiceResult> DeleteUserAsync(int id);
    Task<IServiceResult<IEnumerable<UserEntity>>> GetAllUsersAsync();
}
