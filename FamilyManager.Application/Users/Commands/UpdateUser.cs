using FamilyManager.Application.Common.Exceptions;
using FamilyManager.Application.Common.Interfaces;
using FamilyManager.Domain.Enums;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyManager.Application.Users.Commands
{
    public record UpdateUserCommand : IRequest
    {
        public Guid Id { get; set; }
        public Country Country { get; set; }
        public string Email { get; set; }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Users.FindAsync([request.Id], cancellationToken);
            //Что если entity не была найдена?
            if (entity.Email != request.Email)
            {
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email,
                    cancellationToken);

                if (existingUser != null)
                {
                    var validationFailure = new ValidationFailure("Email", "Email is already taken by another user.");

                    throw new ValidationException([validationFailure]);
                }
            }

            entity.Country = request.Country;
            entity.Email = request.Email;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
