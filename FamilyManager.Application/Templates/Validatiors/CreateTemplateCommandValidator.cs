using FamilyManager.Application.Common.Interfaces;
using FamilyManager.Application.Templates.Commands;
using FluentValidation;

namespace FamilyManager.Application.Templates.Validatiors
{
    public class CreateTemplateCommandValidator : AbstractValidator<CreateTemplateCommand>
    {
        private readonly IApplicationDbContext _context;
        public CreateTemplateCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(t => t.Name)
                .NotEmpty().WithMessage("Name is required.");

            RuleFor(t => t.Description)
                .NotEmpty().WithMessage("Template description is required.");

            RuleFor(t => t.Section)
                 .IsInEnum().WithMessage("Section is requires");


        }
    }
}
