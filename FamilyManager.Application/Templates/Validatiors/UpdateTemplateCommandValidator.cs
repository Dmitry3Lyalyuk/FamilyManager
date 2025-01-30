using FamilyManager.Application.Templates.Commands.Update;
using FluentValidation;

namespace FamilyManager.Application.Templates.Validatiors
{
    public class UpdateTemplateCommandValidator : AbstractValidator<UpdateTemplateCommand>
    {
        public UpdateTemplateCommandValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(40)
                .NotEmpty().
                WithMessage("Name is required.");

            RuleFor(x => x.Description)
                .NotEmpty().
                WithMessage("Description is required");
        }
    }
}
