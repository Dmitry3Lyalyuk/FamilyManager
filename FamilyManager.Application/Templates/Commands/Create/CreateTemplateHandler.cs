using FamilyManager.Application.Common.Interfaces;
using FamilyManager.Domain.Entities;
using MediatR;

namespace FamilyManager.Application.Templates.Commands.Create
{
    public class CreateTemplateCommandHandler : IRequestHandler<CreateTemplateCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        public CreateTemplateCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateTemplateCommand request, CancellationToken cancellationToken)
        {
            var template = new Template()
            {
                Name = request.Name,
                Description = request.Description,
                Section = request.Section,
                UserId = request.UserId,
                FamilyId = request.FamilyId
            };

            _context.Templates.Add(template);
            await _context.SaveChangesAsync(cancellationToken);

            return template.Id;
        }
    }

}
