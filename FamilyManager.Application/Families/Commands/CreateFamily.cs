using FamilyManager.Application.Common.Interfaces;
using FamilyManager.Domain.Entities;
using FamilyManager.Domain.Enums;
using MediatR;

namespace FamilyManager.Application.Families.Commands
{
    public class CreateFamilyCommand : IRequest<Guid>
    {
        public Category Category { get; set; }
        public string Name { get; set; }
        public string Brand { get; init; }

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
                    Category = request.Category,
                    Brand = request.Brand
                };
                _context.Families.Add(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}