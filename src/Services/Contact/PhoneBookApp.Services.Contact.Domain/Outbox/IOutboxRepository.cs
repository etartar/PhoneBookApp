using PhoneBookApp.Core.Domain;

namespace PhoneBookApp.Services.Contact.Domain.Outbox;

public interface IOutboxRepository
{
    Task AddDomainEventAsync(DomainEvent domainEvent, CancellationToken cancellationToken = default);
}
