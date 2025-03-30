using GroupApp.API.Results;
using GroupApp.Data;

namespace GroupApp.API;

public interface ITaskService
{
    Task<IServiceResult<IEnumerable<TaskEntity>>> GetAllTasksAsync();
    Task<IServiceResult<TaskEntity>> GetTaskByIdAsync(int id);
    Task<IServiceResult> CreateTaskAsync(TaskDTO dto);
    Task<IServiceResult> DeleteTaskAsync(int id);
    Task<IServiceResult> UpdateTaskAsync(TaskDTO dto, int id);
}