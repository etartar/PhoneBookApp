using Microsoft.EntityFrameworkCore.Infrastructure;

namespace PhoneBookApp.Core.Application.Abstractions;

public interface IUnitOfWork
{
    DatabaseFacade Database { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
