using FamilyManager.Domain.Enums;
using MediatR;

public record UpdateUserCommand : IRequest
{
	public Guid Id { get; set; }
	public Country Country { get; set; }
	public string Email { get; set; }
}