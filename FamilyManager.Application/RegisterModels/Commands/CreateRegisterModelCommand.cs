using FamilyManager.Application.Common.Interfaces;
using FamilyManager.Domain.Entities;
using FamilyManager.Domain.Enums;
using FluentValidation;
using MediatR;

namespace FamilyManager.Application.RegisterModels.Commands
{
    public record CreateRegisterModelCommand : IRequest<Guid>
    {
        public Status Status { get; set; }
        public Country Country { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class CreateRegisterModelHandler : IRequestHandler<CreateRegisterModelCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly IValidator<CreateRegisterModelCommand> _validator;
        public CreateRegisterModelHandler(IApplicationDbContext context, IValidator<CreateRegisterModelCommand> validator)
        {
            _context = context;
            _validator = validator;
        }
        public async Task<Guid> Handle(CreateRegisterModelCommand request, CancellationToken cancellationToken)
        {
            var entity = new RegisterModel()
            {
                Status = request.Status,
                Country = request.Country,
                UserName = request.UserName,
                Email = request.Email,
                Password = request.Password
            };
            _context.RegisterModels.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
