using PhoneBookApp.Core.EventBus;
using PhoneBookApp.Shared.Dtos;

namespace PhoneBookApp.Shared.IntegrationEvents;

public sealed class ReportGeneratedIntegrationEvent : IntegrationEvent
{
    public ReportGeneratedIntegrationEvent(
        Guid id,
        DateTime occurredOnUtc,
        Guid reportId,
        List<ReportDetailDto> reportDetails) : base(id, occurredOnUtc)
    {
        ReportId = reportId;
        ReportDetails = reportDetails;
    }

    public Guid ReportId { get; set; }
    public List<ReportDetailDto> ReportDetails { get; set; }
}