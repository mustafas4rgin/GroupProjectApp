using FluentValidation;
using GroupApp.API.Results;
using GroupApp.Data;
using Microsoft.EntityFrameworkCore;

namespace GroupApp.API;

public class UserService : IUserService
{
    private readonly IDataRepository _dataRepository;
    private readonly IValidator<UserDTO> _validator;

    public UserService(IDataRepository dataRepository, IValidator<UserDTO> validator)
    {
        _dataRepository = dataRepository;
        _validator = validator;
    }

    public async Task<IServiceResult<UserEntity>> GetUserByIdAsync(int id)
    {
        var user = await _dataRepository.GetAll<UserEntity>()
                    .Include(U => U.Role)
                    .FirstOrDefaultAsync();
                    
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
    public async Task<IServiceResult<IEnumerable<UserEntity>>> GetAllUsersAsync()
    {
        var users = await _dataRepository.GetAll<UserEntity>().
                            Include(U => U.Role)
                            .ToListAsync();

        if(users == null || users.Count == 0)
           return new ServiceResult<IEnumerable<UserEntity>>(false,"There is no user.",new List<UserEntity>());

        return new ServiceResult<IEnumerable<UserEntity>>(true,"There is users.",users);
    }
    
    public async Task<IServiceResult> CreateUserAsync(UserDTO dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);

        if (!validationResult.IsValid)
        {
            return new ServiceResult(false, validationResult.Errors.First().ErrorMessage);
        }

        var userRole = await _dataRepository.GetByIdAsync<RoleEntity>(dto.RoleId);

        if(userRole == null)
            return new ServiceResult(false, "There is no role.");
        
        var user = MappingHelper.MapToUserEntity(dto);
        await _dataRepository.AddAsync(user);
        return new ServiceResult
        {
            Success = true,
            Message = "User created successfully"
        };
    }
    public async Task<IServiceResult> UpdateUserAsync(int id, UserDTO dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);

        if (!validationResult.IsValid)
        {
            return new ServiceResult(false, validationResult.Errors.First().ErrorMessage);
        }

        var userRole = await _dataRepository.GetByIdAsync<RoleEntity>(dto.RoleId);

        if (userRole == null)
            return new ServiceResult(false,"There is no role.");

        var updatingUser = await _dataRepository.GetByIdAsync<UserEntity>(id);

        if (updatingUser == null)
            return new ServiceResult(false, "User not found");

        updatingUser.FirstName = dto.FirstName;
        updatingUser.LastName = dto.LastName;
        updatingUser.Email = dto.PhoneNumber;
        updatingUser.RoleId = dto.RoleId;
        updatingUser.ProfilePicture = dto.ProfilePicture;

        await _dataRepository.UpdateAsync(updatingUser);

        return new ServiceResult(true,"User updated");
    }
    public async Task<IServiceResult> DeleteUserAsync(int id)
    {
        var deletingUser = await _dataRepository.GetByIdAsync<UserEntity>(id);

        if (deletingUser == null)
            return new ServiceResult(false, "User not found");

        await _dataRepository.DeleteAsync<UserEntity>(id);

        return new ServiceResult(true,"User deleted");
    }
}
