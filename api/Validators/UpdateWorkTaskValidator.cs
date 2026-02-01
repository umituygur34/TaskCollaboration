using FluentValidation;
using TaskCollaboration.Api.DTOs.WorkTaskDto;

namespace TaskCollaboration.Api.Validators
{
    public class UpdateWorkTaskValidator : AbstractValidator<UpdateWorkTaskDto>
    {
        public UpdateWorkTaskValidator()
        {

            RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MinimumLength(3).WithMessage("Title must be at least 3 characters long.")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

            RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

        }

    }
}