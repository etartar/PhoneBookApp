using PhoneBookApp.Services.Report.Domain.Reports;

namespace PhoneBookApp.Services.Report.Application.Reports.GetReports;

public class GetReportsDto
{
    public Guid Id { get; set; }
    public DateTime RequestDate { get; set; }
    public ReportStatuses ReportStatus { get; set; }
}