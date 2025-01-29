using FamilyManager.Domain.Enums;

namespace FamilyManager.Application.Users.Queries
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public Country Country { get; set; }
        public string Email { get; set; }
    }
}
