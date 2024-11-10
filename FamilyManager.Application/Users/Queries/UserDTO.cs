using System.Data;

namespace FamilyManager.Application.Users.Querries
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DataSetDateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime LastModifiedAt { get; set; }
        public Guid LastModifiedBy { get; set; }
    }
}
