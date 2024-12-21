using FamilyManager.Application.RegisterModels.Commands;
using FluentValidation;

namespace FamilyManager.Web.Validators
{
    public class CreateRegisterModelValidator : AbstractValidator<CreateRegisterModelCommand>
    {
        public CreateRegisterModelValidator()
        {
            RuleFor(s => s.Status)
                .IsInEnum().WithMessage("Status is required. Choose one from list below.");

            RuleFor(c => c.Country)
                .IsInEnum().WithMessage("Country is required. Choose one from list below.");

            RuleFor(un => un.UserName)
                .MinimumLength(3)
                .MaximumLength(20)
                .NotEmpty().WithMessage("Valid UserName is required.");

            RuleFor(em => em.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email should have a valid email format");

            RuleFor(pass => pass.Password)
                .NotEmpty().WithMessage("Password s required")
                .MinimumLength(5).WithMessage("Password must be at least 5 characters long")
                .MaximumLength(30).WithMessage("Password cannot exceed 20 characters");
        }
    }
}
