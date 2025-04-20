using PhoneBookApp.Core.Domain;

namespace PhoneBookApp.Services.Contact.Domain.Reports;

public class ReportGenerateDomainEvent(Guid reportId) : DomainEvent
{
    public Guid ReportId { get; set; } = reportId;
}
