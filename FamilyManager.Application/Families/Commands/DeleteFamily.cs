using FamilyManager.Application.Common.Interfaces;
using MediatR;

namespace FamilyManager.Application.Familys.Commands
{
    public record DeleteFamilyCommand(Guid Id) : IRequest;

    public class DeleteFalilyCommandHandler : IRequestHandler<DeleteFamilyCommand>
    {
        private readonly IApplicationDbContext _context;
        public DeleteFalilyCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteFamilyCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Families.FindAsync([request.Id], cancellationToken);

            _context.Families.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

