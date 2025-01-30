using FamilyManager.Application.Families.Commands.Create;
using FluentValidation;

namespace FamilyManager.Application.Families.Validations
{
    public class CreateFamilyCommandValidator : AbstractValidator<CreateFamilyCommand>
    {
        public CreateFamilyCommandValidator()
        {
            RuleFor(f => f.Name)
                .NotEmpty()
                .WithMessage("Name is required.");

            RuleFor(f => f.Category)
                .IsInEnum()
                .WithMessage("Category is required");
        }
    }
}
