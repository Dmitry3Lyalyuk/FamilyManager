using FamilyManager.Domain.Enums;
using MediatR;
namespace FamilyManager.Application.Templates.Commands.Create
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
}
