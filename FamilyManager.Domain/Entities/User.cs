using FamilyManager.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace FamilyManager.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public Status Status { get; set; }
        public string Role { get; set; }
        public Country Country { get; set; }
    }
}
