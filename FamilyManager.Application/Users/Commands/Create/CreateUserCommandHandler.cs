using FamilyManager.Application.Common.Interfaces;
using FamilyManager.Domain.Entities;
using FluentValidation;
using MediatR;

namespace FamilyManager.Application.Users.Commands
{
  
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
