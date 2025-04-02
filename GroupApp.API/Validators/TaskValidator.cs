using FluentValidation;
using GroupApp.Data;

namespace GroupApp.API.Validators;

    public class TaskValidator : AbstractValidator<TaskDTO>
    {
        public TaskValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(50).WithMessage("Title cannot be longer than 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description cannot be longer than 500 characters.");

            RuleFor(x => x.DueDate)
                .GreaterThanOrEqualTo(DateTime.Now).WithMessage("Due date must be in the future.");

            RuleFor(x => x.CreatedByUserId)
                .GreaterThan(0).WithMessage("CreatedByUserId must be greater than zero.");
        }
    }
