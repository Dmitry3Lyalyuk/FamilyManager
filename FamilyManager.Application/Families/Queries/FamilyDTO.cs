using FamilyManager.Domain.Enums;

namespace FamilyManager.Application.Families.Queries
{
    public class FamilyDTO
    {
        public Guid Id { get; set; }
        public Category Category { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
    }
}
