using FamilyManager.Application.Common.Interfaces;
using MediatR;

namespace FamilyManager.Application.Templates.Commands
{
    public record UpdateTemplateCommand : IRequest
    {
        public Guid TemplateId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class UpdateTemplateCommandHandler : IRequestHandler<UpdateTemplateCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateTemplateCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateTemplateCommand request, CancellationToken cancellationToken)
        {
            var template = await _context.Templates.FindAsync(request.TemplateId);

            if (template == null)
            {
                throw new Exception($"Entity with Id={request.TemplateId} was not found");
            }

            template.Name = request.Name;
            template.Description = request.Description;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
