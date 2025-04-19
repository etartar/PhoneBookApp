using MongoDB.Bson.Serialization.Attributes;
using PhoneBookApp.Core.Domain;
using PhoneBookApp.Services.Report.Domain.ReportDetails;

namespace PhoneBookApp.Services.Report.Domain.Reports;

public class Report : Entity
{
    public Report()
    {
    }

    public Report(DateTime requestDate, ReportStatuses reportStatus)
    {
        RequestDate = requestDate;
        ReportStatus = reportStatus;
    }

    public Guid Id { get; set; }

    [BsonElement("request_date")]
    public DateTime RequestDate { get; set; }

    [BsonElement("report_status")]
    public ReportStatuses ReportStatus { get; set; }

    public virtual ICollection<ReportDetail> ReportDetails { get; set; } = new HashSet<ReportDetail>();

    public static Report Create(DateTime requestDate, ReportStatuses reportStatus)
    {
        Report createReport = new Report(requestDate, reportStatus);

        //createReport.Raise(new ReportRequestCreatedEvent(createReportRequest.Id));

        return createReport;
    }
}
