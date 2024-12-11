using FamilyManager.Domain.Entities;

namespace FamilyManager.Infrastructure.Extensions
{
    public static class RoleExtensions
    {
        public static string ToIdentityRole(this ApplicationRole role)
        {
            return role.ToString();
        }
    }
}
