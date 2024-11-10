using FamilyManager.Application.Common;
using FamilyManager.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;


namespace FamilyManager.Infrastructure.Data.Interseptors
{
    public class AuditableEntityInterceptor : SaveChangesInterceptor
    {
        private readonly TimeProvider _dateTime;
        private readonly IUser _user;
        public AuditableEntityInterceptor(TimeProvider dateTime, IUser user)
        {
            _dateTime = dateTime;
            _user = user;
        }
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData,
            InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);

            return base.SavingChanges(eventData, result);
        }
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public void UpdateEntities(DbContext? context)
        {
            if (context == null)
            {
                return;
            }
            foreach (var entry in context.ChangeTracker.Entries<BaseAuditableEntity>())
            {
                if (entry.State is EntityState.Added or EntityState.Modified
                    || entry.HasChangesOwnedEntities())
                {
                    var utcNow = _dateTime.GetUtcNow();

                    var userId = _user.GetCurrentUser();

                    if (entry.State == EntityState.Added)
                    {
                        entry.Entity.CreatedBy = userId;
                        entry.Entity.CreatedAt = utcNow.DateTime;
                    }
                    entry.Entity.LastModifiedBy = userId;
                    entry.Entity.LastModifiedAt = utcNow.DateTime;
                }
            }
        }
    }

    public static class Extensions
    {
        public static bool HasChangesOwnedEntities(this EntityEntry entry)
        {
            return entry.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
        }

    }
}
