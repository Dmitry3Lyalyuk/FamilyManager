using MediatR;


namespace FamilyManager.Application.Families.Commands.Delete
{
    public record DeleteFamilyCommand(Guid Id) : IRequest;
}
