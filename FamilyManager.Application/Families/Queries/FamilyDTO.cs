using FamilyManager.Domain.Enums;

namespace FamilyManager.Application.Families.Querries //Не совпадает со структурой папок, Queries
{
    public class FamilyDTO
    {
        public Guid Id { get; set; }
        public Category Category { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
    }
}
