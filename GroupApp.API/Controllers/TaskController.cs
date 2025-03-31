using FluentValidation;
using GroupApp.Core.Concrete;
using GroupApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : GenericController<TaskEntity>
    {
        public TaskController(IService<TaskEntity> service, IValidator<TaskEntity> validator) :base(service, validator)
        {}
    }
}
