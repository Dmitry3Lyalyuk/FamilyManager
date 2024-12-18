using FamilyManager.Domain.Enums;

namespace FamilyManager.Web.Models
{
    public record RegisterModel
    {
        public Status Status { get; set; }
        public Country Country { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
