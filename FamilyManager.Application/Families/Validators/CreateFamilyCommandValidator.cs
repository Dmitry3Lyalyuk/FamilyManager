using FamilyManager.Application.Families.Commands;
using FluentValidation;

namespace FamilyManager.Application.Familys.Validations //Не совпадает со структурой папок, Families
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
