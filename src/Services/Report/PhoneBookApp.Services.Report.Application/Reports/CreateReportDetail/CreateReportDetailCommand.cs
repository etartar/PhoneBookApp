using PhoneBookApp.Core.Application.Messaging;
using PhoneBookApp.Shared.Dtos;

namespace PhoneBookApp.Services.Report.Application.Reports.CreateReportDetail;

public class CreateReportDetailCommand : ICommand
{
    public CreateReportDetailCommand(Guid reportId, List<ReportDetailDto> reportDetails)
    {
        ReportId = reportId;
        ReportDetails = reportDetails;
    }

    public Guid ReportId { get; set; }
    public List<ReportDetailDto> ReportDetails { get; set; }
}
