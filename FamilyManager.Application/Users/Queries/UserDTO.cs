using FamilyManager.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FamilyManager.Application.Users.Queries
{
    public class UserDTO
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public Country Country { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
