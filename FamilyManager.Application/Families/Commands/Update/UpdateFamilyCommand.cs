using MediatR;

namespace FamilyManager.Application.Families.Commands.Update
{
    public record UpdateFamilyCommand : IRequest
    {
        public Guid Id { get; init; }
        public string Brand { get; init; }
        public string Name { get; init; }
    }
}
