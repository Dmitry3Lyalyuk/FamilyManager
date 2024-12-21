using FamilyManager.Domain.Common;
using FamilyManager.Domain.Enums;

namespace FamilyManager.Domain.Entities
{
    public class RegisterModel : BaseAuditableEntity
    {
        public Status Status { get; set; }
        public Country Country { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
