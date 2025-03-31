using GroupApp.Core.Results;
using GroupApp.Data;

namespace GroupApp.Core.Concrete;

public interface IAuthService
{
    Task<IServiceResult> RegisterAsync(UserDTO dto);
    Task<IServiceResult<UserEntity>> LoginAsync(LoginDTO dto);
}