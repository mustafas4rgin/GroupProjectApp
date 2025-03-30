using GroupApp.API.Results;
using GroupApp.Data;

namespace GroupApp.API;

public interface IRoleService
{
    Task<IServiceResult<RoleEntity>> GetRoleByIdAsync(int id);
    Task<IServiceResult<IEnumerable<RoleEntity>>> GetAllRolesAsync();
    Task<IServiceResult> CreateRoleAsync(RoleDTO roleDto);
    Task<IServiceResult> UpdateRoleAsync(RoleDTO roleDto, int id);
    Task<IServiceResult> DeleteRoleAsync(int id);
}
