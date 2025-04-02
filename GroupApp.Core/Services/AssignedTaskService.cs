using GroupApp.Core.Concrete;
using GroupApp.Core.Results;
using GroupApp.Data;
using Microsoft.EntityFrameworkCore;
using static GroupApp.Core.Services.AssignedTaskService;

namespace GroupApp.Core.Services;

public class AssignedTaskService : Service<TaskRelEntity>, IAssignedTaskService
{
    private readonly IRepository<TaskRelEntity> _repository;
    public AssignedTaskService(IRepository<TaskRelEntity> repository) : base(repository)
    {
        _repository = repository;
    }
    public async Task<IServiceResult<IEnumerable<TaskRelEntity>>> ListAssignedTasks(int userId)
    {
        var assignedTasks = await _repository.GetAllAsync()
            .Include(t => t.Task)
            .Include(t => t.User)
            .Where(t => t.UserId == userId)
            .ToListAsync();

        if (assignedTasks is null || !assignedTasks.Any())
            return new ServiceResult<IEnumerable<TaskRelEntity>>
            {
                Message = "No assigned tasks found.",
                Data = null,
                Success = false,
            };
        
        return new ServiceResult<IEnumerable<TaskRelEntity>>
        {
            Success = true,
            Message = "Assigned tasks found.",
            Data = assignedTasks,
        };
    }
public interface IAssignedTaskService : IService<TaskRelEntity>
{
    Task<IServiceResult<IEnumerable<TaskRelEntity>>> ListAssignedTasks(int userId);
}}