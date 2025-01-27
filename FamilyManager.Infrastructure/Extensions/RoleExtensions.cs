using FamilyManager.Domain.Enums;

namespace FamilyManager.Infrastructure.Extensions
{
    public static class RoleExtensions
    {
        //Если расширение используется крайне редко (здесь только в AuthController)
        //от него можно избавиться
        public static string ToIdentityRole(this ApplicationRole role)
        {
            return role.ToString();
        }
    }
}
