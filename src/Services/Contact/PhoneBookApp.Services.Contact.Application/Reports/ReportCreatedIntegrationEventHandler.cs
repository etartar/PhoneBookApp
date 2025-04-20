using Microsoft.Extensions.Logging;
using PhoneBookApp.Core.Application.Abstractions;
using PhoneBookApp.Core.EventBus;
using PhoneBookApp.Services.Contact.Domain.Outbox;
using PhoneBookApp.Services.Contact.Domain.Reports;
using PhoneBookApp.Shared.IntegrationEvents;

namespace PhoneBookApp.Services.Contact.Application.Reports;

internal sealed class ReportCreatedIntegrationEventHandler(
    ILogger<ReportCreatedIntegrationEventHandler> logger,
    IOutboxRepository outboxRepository,
    IUnitOfWork unitOfWork) : IntegrationEventHandler<ReportCreatedIntegrationEvent>
{
    public override async Task Handle(ReportCreatedIntegrationEvent integrationEvent, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{Event} consumed.", nameof(ReportCreatedIntegrationEvent));

        var domainEvent = new ReportGenerateDomainEvent(integrationEvent.ReportId);

        await outboxRepository.AddDomainEventAsync(domainEvent, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
