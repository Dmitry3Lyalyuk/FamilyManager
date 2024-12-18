using FamilyManager.Application.Common.Interfaces;
using FamilyManager.Domain.Entities;
using FamilyManager.Domain.Enums;
using FluentValidation;
using MediatR;

namespace FamilyManager.Application.Users.Commands
{
    public record CreateUserCommand : IRequest<Guid>
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
        private readonly IValidator<CreateUserCommand> _validator;
        public CreateUserCommandHandler(IApplicationDbContext context, IValidator<CreateUserCommand> validator)
        {
            _context = context;
            _validator = validator;
        }
        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = new User
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
