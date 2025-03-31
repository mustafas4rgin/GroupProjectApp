using System.Threading.Tasks;
using FluentValidation;
using GroupApp.Core.Concrete;
using GroupApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
[ApiController]
public class UserController : GenericController<UserEntity>
{

    public UserController(IService<UserEntity> service, IValidator<UserEntity> validator)
        : base(service, validator)
    {
    }
}

}
