using FluentValidation;
using GroupApp.Core.Concrete;
using GroupApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : GenericController<RoleEntity>
    {
        public RoleController(IService<RoleEntity> service, IValidator<RoleEntity> validator) : base(service, validator)
        {}
    }
}
