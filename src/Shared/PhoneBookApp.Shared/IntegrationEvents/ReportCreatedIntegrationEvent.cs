using PhoneBookApp.Core.EventBus;

namespace PhoneBookApp.Shared.IntegrationEvents;

public sealed class ReportCreatedIntegrationEvent : IntegrationEvent
{
    public ReportCreatedIntegrationEvent(
        Guid id,
        DateTime occurredOnUtc,
        Guid reportId) : base(id, occurredOnUtc)
    {
        ReportId = reportId;
    }

    public Guid ReportId { get; set; }
}
