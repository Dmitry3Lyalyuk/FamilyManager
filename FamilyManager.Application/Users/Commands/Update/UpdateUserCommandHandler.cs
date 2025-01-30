using FamilyManager.Application.Common.Exceptions;
using FamilyManager.Application.Common.Interfaces;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyManager.Application.Users.Commands
{
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

            if (entity is null)
            {
                throw new ValidationException(new[] { new ValidationFailure("Id", $"User with id {request.Id} not found") });
            }

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
