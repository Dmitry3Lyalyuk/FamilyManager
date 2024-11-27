using FamilyManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FamilyManager.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Family> Families { get; }
        DbSet<Template> Templates { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
