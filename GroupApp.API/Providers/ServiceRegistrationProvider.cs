namespace GroupApp.API;

public class ServiceRegistrationProvider
{
    public static void RegisterServices(IServiceCollection services)
    {
        var servicesToRegister = new (Type Interface, Type Implementation)[]
        {
            (typeof(IUserService), typeof(UserService)),
            (typeof(IDataRepository), typeof(DataRepository)),
            (typeof(IRoleService), typeof(RoleService)),
        };

        foreach (var service in servicesToRegister)
        {
            services.AddTransient(service.Interface, service.Implementation);
        }
    }
}
