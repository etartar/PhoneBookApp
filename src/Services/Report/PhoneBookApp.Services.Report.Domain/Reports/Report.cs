using PhoneBookApp.Core.Domain;
using PhoneBookApp.Services.Report.Domain.ReportDetails;

namespace PhoneBookApp.Services.Report.Domain.Reports;

public class Report : Entity
{
    public Report()
    {
    }

    public Report(Guid id, DateTime requestDate, ReportStatuses reportStatus)
    {
        Id = id;
        RequestDate = requestDate;
        ReportStatus = reportStatus;
    }

    public Guid Id { get; set; }
    public DateTime RequestDate { get; set; }
    public ReportStatuses ReportStatus { get; set; }

    public virtual ICollection<ReportDetail> ReportDetails { get; set; } = new HashSet<ReportDetail>();

    public static Report Create(DateTime requestDate, ReportStatuses reportStatus)
    {
        Report createReport = new Report(Guid.NewGuid(), requestDate, reportStatus);

        createReport.Raise(new ReportCreatedDomainEvent(createReport.Id));

        return createReport;
    }

    public void ReportGenerated()
    {
        ReportStatus = ReportStatuses.Completed;
    }
}
