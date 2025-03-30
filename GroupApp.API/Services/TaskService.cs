using System.Threading.Tasks;
using FluentValidation;
using GroupApp.API.Helpers;
using GroupApp.API.Results;
using GroupApp.Data;
using Microsoft.EntityFrameworkCore;

namespace GroupApp.API.Services;

public class TaskService : ITaskService
{
    private readonly IDataRepository _dataRepository;
    private readonly IValidator<TaskDTO> _validator;

    public TaskService(IDataRepository dataRepository, IValidator<TaskDTO> validator)
    {
        _dataRepository = dataRepository;
        _validator = validator;
    }

    public async Task<IServiceResult<IEnumerable<TaskEntity>>> GetAllTasksAsync()
    {
        var tasks = await _dataRepository.GetAll<TaskEntity>().ToListAsync();

        if (!tasks.Any() || tasks is null)
            return new ServiceResult<IEnumerable<TaskEntity>>(false, "There is no task.", new List<TaskEntity>());

        return new ServiceResult<IEnumerable<TaskEntity>>(true, "Tasks", tasks);
    }
    public async Task<IServiceResult<TaskEntity>> GetTaskByIdAsync(int id)
    {
        var task = await _dataRepository.GetByIdAsync<TaskEntity>(id);

        if (task is null)
            return new ServiceResult<TaskEntity>
            {
                Success = false,
                Message = "Task not found"
            };

        return new ServiceResult<TaskEntity>
        {
            Success = true,
            Message = "Task",
            Data = task,
        };
    }
    public async Task<IServiceResult> CreateTaskAsync(TaskDTO dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);

        if (!validationResult.IsValid)
            return new ServiceResult(false, validationResult.Errors.First().ErrorMessage);

        var task = MappingHelper.MapToTaskEntity(dto);

        await _dataRepository.AddAsync(task);

        return new ServiceResult
        {
            Success = true,
            Message = "Task created."
        };
    }
    public async Task<IServiceResult> DeleteTaskAsync(int id)
    {
        var deletingTask = await _dataRepository.GetByIdAsync<TaskEntity>(id);

        if (deletingTask is null)
            return new ServiceResult
            {
                Success = false,
                Message = "There is no task with this id."
            };

        await _dataRepository.DeleteAsync<TaskEntity>(id);

        return new ServiceResult
        {
            Success = true,
            Message = "Task deleted successfully."
        };
    }
    public async Task<IServiceResult> UpdateTaskAsync(TaskDTO dto, int id)
    {
        var updatingTask = await _dataRepository.GetByIdAsync<TaskEntity>(id);

        if (updatingTask is null)
            return new ServiceResult
            {
                Success = false,
                Message = "There is no task with this id."
            };

        var validationResult = await _validator.ValidateAsync(dto);

        if (!validationResult.IsValid)
            return new ServiceResult(false, validationResult.Errors.First().ErrorMessage);

        updatingTask.CreatedByUserId = dto.CreatedByUserId;
        updatingTask.Title = dto.Title;
        updatingTask.Description = dto.Description;
        updatingTask.DueDate = dto.DueDate;
        updatingTask.IsCompleted = dto.IsCompleted;

        return new ServiceResult
        {
            Success = true,
            Message = "Task updated."
        };
    }
}