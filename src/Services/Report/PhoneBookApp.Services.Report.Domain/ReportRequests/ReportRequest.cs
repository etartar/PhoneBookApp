using PhoneBookApp.Core.Domain;
using PhoneBookApp.Services.Report.Domain.Reports;

namespace PhoneBookApp.Services.Report.Domain.ReportRequests;

public class ReportRequest : Entity
{
    public ReportRequest()
    {
    }

    public ReportRequest(DateTime requestDate, ReportStatuses reportStatus)
    {
        RequestDate = requestDate;
        ReportStatus = reportStatus;
    }

    public Guid Id { get; set; }
    public DateTime RequestDate { get; set; }
    public ReportStatuses ReportStatus { get; set; }

    public virtual ICollection<Reports.Report> Reports { get; set; } = new HashSet<Reports.Report>();

    public static ReportRequest Create(DateTime requestDate, ReportStatuses reportStatus)
    {
        ReportRequest createReportRequest = new ReportRequest(requestDate, reportStatus);

        //createReportRequest.Raise(new ReportRequestCreatedEvent(createReportRequest.Id));

        return createReportRequest;
    }
}
