using FamilyManager.Domain.Enums;
using MediatR;

namespace FamilyManager.Web.Requests
{
    public record UserUpdateRequest 
    {
        public string UserName { get; set; }
        public Country Country { get; set; }
        public string Email { get; set; }
    }
}
