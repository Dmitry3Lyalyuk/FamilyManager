using FamilyManager.Application.Common.Interfaces;
using FamilyManager.Application.Templates.Commands.Create;
using FluentValidation;

namespace FamilyManager.Application.Templates.Validatiors
{
    public class CreateTemplateCommandValidator : AbstractValidator<CreateTemplateCommand>
    {
        public CreateTemplateCommandValidator()
        {
            
            RuleFor(t => t.Name)
                .NotEmpty().WithMessage("Name is required.");

            RuleFor(t => t.Description)
                .NotEmpty().WithMessage("Template description is required.");

            RuleFor(t => t.Section)
                 .IsInEnum().WithMessage("Section is requires");

            RuleFor(t => t.FamilyId)
                .NotEmpty().WithMessage("Family Id is required");

            RuleFor(t => t.UserId)
                .Must(id => id == null || id != Guid.Empty).WithMessage("Valid UserId is required");
        }
    }
}
