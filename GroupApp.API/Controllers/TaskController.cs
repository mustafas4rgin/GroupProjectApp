using AutoMapper;
using FluentValidation;
using GroupApp.Core.Concrete;
using GroupApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : GenericController<TaskEntity, TaskDTO>
    {
        public TaskController(IService<TaskEntity> service, IValidator<TaskDTO> validator, IMapper mapper)
            : base(service, validator, mapper)
        {
        }
    }
}
