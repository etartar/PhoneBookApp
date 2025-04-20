using Newtonsoft.Json;
using PhoneBookApp.Core.Domain;
using PhoneBookApp.Core.Infrastructure.Outbox;
using PhoneBookApp.Core.Infrastructure.Serialization;
using PhoneBookApp.Services.Contact.Domain.Outbox;
using PhoneBookApp.Services.Contact.Infrastructure.Database;

namespace PhoneBookApp.Services.Contact.Infrastructure.Repositories;

internal sealed class OutboxRepository : IOutboxRepository
{
    private readonly ContactDbContext DbContext;

    public OutboxRepository(ContactDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task AddDomainEventAsync(DomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        OutboxMessage outboxMessage = new OutboxMessage
        {
            Id = domainEvent.Id,
            Type = domainEvent.GetType().Name,
            Content = JsonConvert.SerializeObject(domainEvent, SerializerSettings.Instance),
            OccurredOnUtc = domainEvent.OccurredOnUtc
        };

        await DbContext.OutboxMessages.AddAsync(outboxMessage, cancellationToken);
    }
}
