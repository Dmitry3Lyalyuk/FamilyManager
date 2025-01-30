using System.ComponentModel.DataAnnotations;

namespace FamilyManager.Web.Models
{
    public record LoginModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
