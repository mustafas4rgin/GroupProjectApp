using System.Collections;
using FluentValidation;
using GroupApp.API.Results;
using GroupApp.Data;
using Microsoft.EntityFrameworkCore;

namespace GroupApp.API;

public class RoleService : IRoleService
{
    private readonly IDataRepository _dataRepository;
    private readonly IValidator<RoleDTO> _roleValidator;

    public RoleService(IDataRepository dataRepository, IValidator<RoleDTO> roleValidator)
    {
        _dataRepository = dataRepository;
        _roleValidator = roleValidator;
    }
    public async Task<IServiceResult<RoleEntity>> GetRoleByIdAsync(int id)
    {
        var role = await _dataRepository.GetByIdAsync<RoleEntity>(id);
        if (role == null)
        {
            return new ServiceResult<RoleEntity>(false, "Role not found", new RoleEntity());
        }
        return new ServiceResult<RoleEntity>(true, "Role found", role);
    }
    public async Task<IServiceResult<IEnumerable<RoleEntity>>> GetAllRolesAsync()
    {
        var roles = await _dataRepository.GetAll<RoleEntity>().ToListAsync();
        if (roles == null || !roles.Any())
        {
            return new ServiceResult<IEnumerable<RoleEntity>>(false, "No roles found", new List<RoleEntity>());
        }
        return new ServiceResult<IEnumerable<RoleEntity>>(true, "Roles found", roles);
    }
    public async Task<IServiceResult> CreateRoleAsync(RoleDTO dto)
    {
        var validationResult = await _roleValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return new ServiceResult(false, validationResult.Errors.First().ErrorMessage);
        }
        var role = MappingHelper.MapToRoleEntity(dto);
        await _dataRepository.AddAsync(role);
        return new ServiceResult<RoleEntity>(true, "Role created successfully", role);
    }
    public async Task<IServiceResult> UpdateRoleAsync(RoleDTO dto, int id)
    {
        var role = await _dataRepository.GetByIdAsync<RoleEntity>(id);
        if (role == null)
        {
            return new ServiceResult(false, "Role not found");
        }
        var validationResult = await _roleValidator.ValidateAsync(dto);
        if(!validationResult.IsValid)
        {
            return new ServiceResult(false,validationResult.Errors.First().ErrorMessage);
        }
        role.Description = dto.Description;
        role.Name = dto.Name;
        role.Permissions = dto.Permissions;
        await _dataRepository.UpdateAsync(role);
        return new ServiceResult<RoleEntity>(true, "Role updated successfully", role);
    }
    public async Task<IServiceResult> DeleteRoleAsync(int id)
    {
        var deletingRole = await _dataRepository.GetByIdAsync<RoleEntity>(id);
        if(deletingRole is null)
        {
            return new ServiceResult(false, "Role not found");
        }
        await _dataRepository.DeleteAsync<RoleEntity>(id);
        return new ServiceResult(true, "Role deleted successfully");
    }
}
