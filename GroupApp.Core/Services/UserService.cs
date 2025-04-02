using GroupApp.Core.Concrete;
using GroupApp.Core.Results;
using GroupApp.Data;
using Microsoft.EntityFrameworkCore;

namespace GroupApp.Core.Services;

public class UserService : Service<UserEntity>, IUserService
{
    private readonly IRepository<UserEntity> _repository;
    public UserService(IRepository<UserEntity> repository) : base(repository)
    {
        _repository = repository;
    }
    public async Task<IServiceResult<IEnumerable<UserEntity>>> ListUsers()
    {
        var users = await _repository.GetAllAsync().Include(U => U.Role)
                    .Include(U => U.AssignedTasks).ToListAsync();
        if (users is null)
            return new ServiceResult<IEnumerable<UserEntity>>
            {
                Message = "User not found.",
                Data = null,
                Success = false,
            };
        return new ServiceResult<IEnumerable<UserEntity>>
        {
            Success = true,
            Message = "User found.",
            Data = users,
        };
    }
}
public interface IUserService : IService<UserEntity>
{
    Task<IServiceResult<IEnumerable<UserEntity>>> ListUsers();
}