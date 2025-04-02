using FluentValidation;
using GroupApp.Data;

namespace GroupApp.API.Validators;

public class UserValidator : AbstractValidator<UserDTO>
{
    public UserValidator()
    {
        RuleFor(x => x.RoleId).NotEmpty().WithMessage("RoleId can't be null.");
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("First name is required.")
            .Length(2, 50)
            .WithMessage("First name must be between 2 and 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Last name is required.")
            .Length(2, 50)
            .WithMessage("Last name must be between 2 and 50 characters.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.");

    }
}
