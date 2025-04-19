using PhoneBookApp.Core.Domain;
using PhoneBookApp.Services.Report.Domain.Reports;

namespace PhoneBookApp.Services.Report.Domain.ReportRequests;

public class ReportRequest : Entity
{
    public ReportRequest()
    {
    }

    public ReportRequest(Guid id, DateTime requestDate, ReportStatuses reportStatus)
    {
        Id = id;
        RequestDate = requestDate;
        ReportStatus = reportStatus;
    }

    public Guid Id { get; set; }
    public DateTime RequestDate { get; set; }
    public ReportStatuses ReportStatus { get; set; }

    public virtual ICollection<Reports.Report> Reports { get; set; } = new HashSet<Reports.Report>();
}
