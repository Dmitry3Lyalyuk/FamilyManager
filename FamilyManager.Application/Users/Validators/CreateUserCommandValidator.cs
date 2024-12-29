using FamilyManager.Application.Common.Interfaces;
using FamilyManager.Application.Users.Commands;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FamilyManager.Application.Users.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IApplicationDbContext _context;
        public CreateUserCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(u => u.UserName)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(20).WithMessage("Name cannot exceed 20 characters.")
                .MustAsync(BeUniqueUserName).WithMessage("Username must be unique.");

            RuleFor(u => u.Country)
                .IsInEnum().WithMessage("Country is required.");

            RuleFor(u => u.Email)
                 .NotEmpty().WithMessage("Email is required")
                 .EmailAddress().WithMessage("Email should have a valid email format.");
        }
        private async Task<bool> BeUniqueUserName(string username, CancellationToken cancellationToken)
        {
            return !await _context.Users.AnyAsync(u => u.UserName == username, cancellationToken);
        }
    }
}
