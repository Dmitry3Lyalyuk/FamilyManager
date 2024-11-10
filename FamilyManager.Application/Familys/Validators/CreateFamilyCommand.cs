using FamilyManager.Application.Families.Commands;
using FluentValidation;

namespace FamilyManager.Application.Familys.Validations
{
    public class CreateFamilyCommandValidator : AbstractValidator<CreateFamilyCommand>
    {
        public CreateFamilyCommandValidator()
        {
            RuleFor(p => p.Name)
                .MaximumLength(30)
                .NotEmpty();
            RuleFor(c => c.Category)
                .NotEmpty();


        }
    }
}
