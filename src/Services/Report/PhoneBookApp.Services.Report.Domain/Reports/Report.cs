using PhoneBookApp.Services.Report.Domain.ReportRequests;

namespace PhoneBookApp.Services.Report.Domain.Reports;

public class Report
{
    public Report()
    {
    }

    public Report(Guid id, Guid reportRequestId, string location, int totalPersonCount, int totalPhoneNumberCount)
    {
        Id = id;
        ReportRequestId = reportRequestId;
        Location = location;
        TotalPersonCount = totalPersonCount;
        TotalPhoneNumberCount = totalPhoneNumberCount;
    }

    public Guid Id { get; set; }
    public Guid ReportRequestId { get; set; }
    public string Location { get; set; }
    public int TotalPersonCount { get; set; }
    public int TotalPhoneNumberCount { get; set; }

    public virtual ReportRequest ReportRequest { get; set; }
}
