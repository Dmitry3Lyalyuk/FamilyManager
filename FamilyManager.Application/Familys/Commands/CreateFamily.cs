using FamilyManager.Application.Common;
using FamilyManager.Domain.Entities;
using MediatR;

namespace FamilyManager.Application.Families.Commands
{
    public class CreateFamilyCommand : IRequest<Guid>
    {
        public string Category { get; set; }

        public string Name { get; set; }
    }
    public class CreateFamilyCommandHandler : IRequestHandler<CreateFamilyCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        public CreateFamilyCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(CreateFamilyCommand request, CancellationToken cancellationToken)
        {
            var entity = new Family
            {
                Name = request.Name,
                Category = request.Category
            };
            _context.Families.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
