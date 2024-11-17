using FamilyManager.Application.Common.Interfaces;
using FamilyManager.Application.Familys.Commands;
using FluentValidation;

namespace FamilyManager.Application.Familys.Validators
{
    public class UpdateFamilyCommandValidator : AbstractValidator<UpdateFamilyCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateFamilyCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(f => f.Name)
                .NotEmpty()
                .WithMessage("Family name is required.");
            RuleFor(f => f.Brand)
                .MaximumLength(30);
        }

    }
}
