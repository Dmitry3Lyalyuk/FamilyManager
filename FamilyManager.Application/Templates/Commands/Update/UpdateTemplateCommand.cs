using MediatR;

namespace FamilyManager.Application.Templates.Commands.Update
{
    public record UpdateTemplateCommand : IRequest
    {
        public Guid TemplateId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
