using FamilyManager.Application.Common.Interfaces;
using FamilyManager.Domain.Entities;
using FamilyManager.Infrastructure.Data;
using FamilyManager.Web.Services;
using Microsoft.AspNetCore.Identity;

namespace FamilyManager.Web
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole<Guid>>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddExceptionHandler<CustomExceptionHandler>();
            services.AddScoped<IUser, CurrentUserService>();
            services.AddHttpContextAccessor();

            return services;
        }
    }
}
