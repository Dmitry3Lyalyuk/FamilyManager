using FamilyManager.Application.Common.Interfaces;
using FamilyManager.Domain.Entities;
using FamilyManager.Domain.Enums;
using MediatR;

namespace FamilyManager.Application.Templates.Commands
{
    public record CreateTemplateCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Section Section { get; set; }
        public Guid? UserId { get; set; }
        public Guid FamilyId { get; set; }
    }
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
                UserId = Guid.Parse("d05faaf9-e56a-43e3-6b55-08dd1300c0f5"),
                FamilyId = Guid.Parse("ddfefd4d-4a45-4b57-4620-08dd1631db18")
            };
            _context.Templates.Add(template);
            await _context.SaveChangesAsync(cancellationToken);

            return template.Id;
        }
    }
}
