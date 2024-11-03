using Microsoft.Extensions.DependencyInjection;

namespace FamilyManager.Infrastructure.Data
{
    public static class InitialiserExtensions
    {
        public static async Task InitialiseDbAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateAsyncScope();
            var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

            await initialiser.InitialiseAsync();
            await initialiser.SeedAsync();
        }
    }
}
