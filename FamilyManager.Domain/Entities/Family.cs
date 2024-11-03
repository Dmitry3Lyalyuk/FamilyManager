using FamilyManager.Domain.Common;

namespace FamilyManager.Domain.Entities
{
    public class Family : BaseAuditableEntity
    {
        public string Category { get; set; }
        public string? Brand { get; set; }
        public string? Template { get; set; }
        public string Name { get; set; }
    }
}
