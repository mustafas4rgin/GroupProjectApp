using GroupApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GroupApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController(ITaskService taskService) : ControllerBase
    {
        private ITaskService _taskService = taskService;

        [HttpGet("/api/tasks")]
        public async Task<IActionResult> GetAllTasks()
        {
            var result = await _taskService.GetAllTasksAsync();

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
        [HttpGet("/api/tasks{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var result = await _taskService.GetTaskByIdAsync(id);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }
        [HttpPost("/api/create/task")]
        public async Task<IActionResult> CreateTask([FromBody]TaskDTO dto)
        {
            var result = await _taskService.CreateTaskAsync(dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
        [HttpPut("/api/update/task/{id}")]
        public async Task<IActionResult> UpdateTask([FromBody]TaskDTO dto, int id)
        {
            var result = await _taskService.UpdateTaskAsync(dto, id);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
        [HttpDelete("/api/delete/task/{id}")]
        public async Task<IActionResult> DeleteTaskById(int id)
        {
            var result = await _taskService.DeleteTaskAsync(id);

            if(!result.Success)
                return NotFound(result);

            return Ok(result);
        }
    }
}
