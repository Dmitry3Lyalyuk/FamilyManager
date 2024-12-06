using FamilyManager.Domain.Common;
using FamilyManager.Domain.Enums;

namespace FamilyManager.Domain.Entities
{
    public class Template : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public Section Section { get; set; }
        public Guid? UserId { get; set; }
        public User User { get; set; }
        public Guid? FamilyId { get; set; }
        public Family Family { get; set; }

        //public ICollection<Family> Families { get; set; } = [];


    }
}
