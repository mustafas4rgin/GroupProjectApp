using AutoMapper;
using FluentValidation;
using GroupApp.Core.Concrete;
using GroupApp.Core.Services;
using GroupApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static GroupApp.Core.Services.AssignedTaskService;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignedTaskController : GenericController<TaskRelEntity, AssignedTaskDTO>
    {
        private readonly IAssignedTaskService _assignedTaskService;

        public AssignedTaskController(IAssignedTaskService assignedTaskService, IValidator<AssignedTaskDTO> validator, IMapper mapper)
            : base(assignedTaskService, validator, mapper)
        {
            _assignedTaskService = assignedTaskService;
        }
        [HttpGet("/api/assigned-tasks/{userId}")]
        public async Task<IActionResult> GetAssignedTasks(int userId)
        {
            var result = await _assignedTaskService.ListAssignedTasks(userId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
