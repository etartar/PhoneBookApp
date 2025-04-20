using PhoneBookApp.Core.Application.Messaging;
using PhoneBookApp.Core.EventBus;
using PhoneBookApp.Services.Report.Domain.Reports;
using PhoneBookApp.Shared.IntegrationEvents;

namespace PhoneBookApp.Services.Report.Application.Reports.CreateReport;

internal sealed class ReportCreatedDomainEventHandler(IEventBus eventBus) : DomainEventHandler<ReportCreatedDomainEvent>
{
    public override async Task Handle(ReportCreatedDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        await eventBus.PublishAsync(
            new ReportCreatedIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                domainEvent.ReportId),
            cancellationToken);
    }
}
