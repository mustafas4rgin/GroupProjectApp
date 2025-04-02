using GroupApp.Core.Concrete;
using GroupApp.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using static GroupApp.Core.Services.AssignedTaskService;

namespace GroupApp.API;

public class ServiceRegistrationProvider 
{
    public static void RegisterServices(IServiceCollection services)
    {
        var servicesToRegister = new (Type Interface, Type Implementation)[]
        {
            (typeof(IService<>), typeof(Service<>)),
            (typeof(IRepository<>), typeof(Repository<>)),
            (typeof(IUserService), typeof(UserService)),
            (typeof(IAuthService), typeof(AuthService)),
            (typeof(IAssignedTaskService), typeof(AssignedTaskService)),

        };

        foreach (var service in servicesToRegister)
        {
            services.AddTransient(service.Interface, service.Implementation);
        }
    }
}
