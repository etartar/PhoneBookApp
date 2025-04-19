using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PhoneBookApp.Core.Application.Clock;
using PhoneBookApp.Core.Domain;

namespace PhoneBookApp.Core.Infrastructure.Interceptors;

public class UpdateSoftDeletableInterceptorInterceptor : SaveChangesInterceptor
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public UpdateSoftDeletableInterceptorInterceptor(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
        {
            UpdateEntities(eventData.Context, _dateTimeProvider);
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void UpdateEntities(DbContext context, IDateTimeProvider dateTimeProvider)
    {
        IEnumerable<EntityEntry<ISoftDeletable>> entries = context.ChangeTracker.Entries<ISoftDeletable>();

        foreach (EntityEntry<ISoftDeletable> entry in entries)
        {
            if (entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Modified;
                entry.Entity.DeletedOn = dateTimeProvider.UtcNow;
            }
        }
    }
}
