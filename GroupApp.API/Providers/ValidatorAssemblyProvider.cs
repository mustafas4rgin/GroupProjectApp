namespace GroupApp.API.Providers;
using FluentValidation;
using GroupApp.API.Validators;

public class ValidatorAssemblyProvider
{
     public static Type[] GetValidatorAssemblies() 
    {
        return new[]
        {
            typeof(UserDTOValidator),
            typeof(RoleDTOValidator),
            typeof(TaskDTOValidator)
        };
    }
}
