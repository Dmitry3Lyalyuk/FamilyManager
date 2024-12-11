using FamilyManager.Application.Common.Interfaces;
using FamilyManager.Domain.Enums;
using MediatR;

namespace FamilyManager.Application.Users.Commands
{
    public record UpdateUserCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Role { get; set; }
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

            if (entity == null)
            {
                throw new Exception($"Entity with Id={request.Id} was not found.");
            }

            entity.Role = request.Role;
            entity.Country = request.Country;
            entity.Email = request.Email;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
