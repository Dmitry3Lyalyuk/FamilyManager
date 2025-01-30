using MediatR;

namespace FamilyManager.Application.Templates.Commands.Delete
{
    public record DeleteTemplateCommand(Guid Id) : IRequest;
}
