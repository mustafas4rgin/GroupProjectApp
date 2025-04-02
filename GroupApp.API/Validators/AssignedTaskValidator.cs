namespace GroupApp.API.Providers;
using FluentValidation;
using GroupApp.Data;

public class AssignedTaskValidator : AbstractValidator<AssignedTaskDTO>
{
    public AssignedTaskValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.")
            .GreaterThan(0).WithMessage("UserId must be greater than zero.");

        RuleFor(x => x.TaskId)
            .NotEmpty().WithMessage("TaskId is required.")
            .GreaterThan(0).WithMessage("TaskId must be greater than zero.");
    }
}