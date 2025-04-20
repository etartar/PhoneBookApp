using PhoneBookApp.Core.Domain;

namespace PhoneBookApp.Services.Report.Domain.Reports;

public sealed class ReportCreatedDomainEvent(Guid reportId) : DomainEvent
{
    public Guid ReportId { get; set; } = reportId;
}
