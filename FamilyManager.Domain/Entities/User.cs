using FamilyManager.Domain.Common;
using FamilyManager.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FamilyManager.Domain.Entities
{
    public class User : BaseAuditableEntity
    {
        public string UserName { get; set; }
        public Status Status { get; init; }
        public string Role { get; set; }
        public string Country { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}
