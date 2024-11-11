using FamilyManager.Application.Users.Commands;
using FluentValidation;

namespace FamilyManager.Application.Users.Validators
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(u => u.Country)
                .NotEmpty().WithMessage("Country is required.");

            RuleFor(u => u.Email)
                 .NotEmpty().WithMessage("Email is required")
                 .EmailAddress().WithMessage("Email should have a valid email format.");
        }

    }
}
