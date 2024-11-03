using FamilyManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FamilyManager.Application.Common
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Family> Families { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
