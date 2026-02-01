

using FluentValidation;
using TaskCollaboration.Api.DTOs.WorkTaskDto;

namespace TaskCollaboration.Api.Validators
{
    public class CreateWorkTaskValidator : AbstractValidator<CreateWorkTaskDto>
    {
        public CreateWorkTaskValidator()
        {

            RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MinimumLength(5).WithMessage("Title must be at least 5 characters long.");

        }
    }
}