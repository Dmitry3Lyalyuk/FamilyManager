using FamilyManager.Application.Common.Interfaces;
using MediatR;

namespace FamilyManager.Application.Families.Commands.Update
{
    public class UpdateFamilyCommandHandler : IRequestHandler<UpdateFamilyCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateFamilyCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Handle(UpdateFamilyCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Families.FindAsync([request.Id], cancellationToken);

            if (entity == null)
            {
                throw new Exception($"Entity with Id={request.Id} was not found.");
            }

            entity.Brand = request.Brand;
            entity.Name = request.Name;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
