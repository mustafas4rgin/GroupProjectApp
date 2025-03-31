using FluentValidation;
using GroupApp.Data;

namespace GroupApp.API.Validators
{
    public class RoleValidator : AbstractValidator<RoleEntity>
    {
        public RoleValidator()
        {
            RuleFor(role => role.Name)
                .NotEmpty().WithMessage("Role name is required.")
                .Length(2, 50).WithMessage("Role name must be between 2 and 50 characters.");
            
            RuleFor(role => role.Description)
                .MaximumLength(200).WithMessage("Description cannot exceed 200 characters.");
        }
    }
}