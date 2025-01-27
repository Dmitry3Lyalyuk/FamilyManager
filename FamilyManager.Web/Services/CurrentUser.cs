using FamilyManager.Application.Common.Interfaces;
using System.Security.Claims;

namespace FamilyManager.Web.Services
{
    public class CurrentUserService : IUser //Название файла не совпадает с названием класса
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid GetCurrentUser()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);

            return userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId) ?
                userId : Guid.NewGuid();
        }
    }
}
