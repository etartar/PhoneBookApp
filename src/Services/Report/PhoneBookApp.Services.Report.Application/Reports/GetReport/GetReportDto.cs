using PhoneBookApp.Services.Report.Domain.Reports;

namespace PhoneBookApp.Services.Report.Application.Reports.GetReport;

public class GetReportDto
{
    public Guid Id { get; set; }
    public DateTime RequestDate { get; set; }
    public ReportStatuses ReportStatus { get; set; }
    public List<GetReportDetailDto> Details { get; set; } = new List<GetReportDetailDto>();
}
