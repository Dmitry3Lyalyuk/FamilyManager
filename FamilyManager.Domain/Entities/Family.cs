using FamilyManager.Domain.Common;
using FamilyManager.Domain.Enums;

namespace FamilyManager.Domain.Entities
{
    public class Family : BaseAuditableEntity
    {
        public Category Category { get; set; }
        public string? Brand { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public ICollection<Template> Templates { get; set; } = [];
        //public string? Template { get; set; }
    }
}
