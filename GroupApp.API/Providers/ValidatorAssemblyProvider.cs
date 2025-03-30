namespace GroupApp.API.Providers;
using FluentValidation;

public class ValidatorAssemblyProvider
{
     public static Type[] GetValidatorAssemblies() 
    {
        return new[]
        {
            typeof(UserDTOValidator),
        };
    }
}
