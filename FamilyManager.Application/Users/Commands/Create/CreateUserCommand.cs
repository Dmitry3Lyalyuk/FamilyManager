using FamilyManager.Domain.Enums;
using MediatR;

public record CreateUserCommand : IRequest<Guid>
{
	public string UserName { get; set; }
	public Status Status { get; init; }
	public Country Country { get; set; }
	public string Email { get; set; }
}