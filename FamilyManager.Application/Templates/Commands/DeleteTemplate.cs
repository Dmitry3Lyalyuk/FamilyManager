using FamilyManager.Application.Common.Interfaces;
using MediatR;

namespace FamilyManager.Application.Templates.Commands
{
    public record DeleteTemplateCommand(Guid Id) : IRequest;

    public class DeleteTemplateCommandHandler : IRequestHandler<DeleteTemplateCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteTemplateCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteTemplateCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Templates.FindAsync([request.Id], cancellationToken);

            if (entity == null)
            {
                throw new Exception($"Entity with Id={request.Id} was not found.");
            }

            _context.Templates.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
