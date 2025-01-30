using FamilyManager.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FamilyManager.Web.Models
{
    public record RegisterModel
    {
        [Required]
        public Status Status { get; set; }
        [Required]
        public Country Country { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
