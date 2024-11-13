using FamilyManager.Application.Common;
using FamilyManager.Domain.Entities;
using FamilyManager.Domain.Enums;
using MediatR;

namespace FamilyManager.Application.Users.Commands
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public string UserName { get; set; }
        public Status Status { get; init; }
        public string Role { get; set; }
        public Country Country { get; set; }
        public string Email { get; set; }
    }
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        public CreateUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = new User()
            {
                UserName = request.UserName,
                Role = request.Role,
                Country = request.Country,
                Email = request.Email,
                Status = request.Status
            };
            _context.Users.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
