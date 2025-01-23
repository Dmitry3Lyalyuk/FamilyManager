using FamilyManager.Domain.Enums;

namespace FamilyManager.Web.Requests
{
    public record UserUpdateRequest
    {
        public Country Country { get; set; }
        public string Email { get; set; }
    }
}
