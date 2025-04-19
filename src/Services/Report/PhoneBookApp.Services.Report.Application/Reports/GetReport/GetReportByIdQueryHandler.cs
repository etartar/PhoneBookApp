using Mapster;
using PhoneBookApp.Core.Application.Messaging;
using PhoneBookApp.Core.Domain;
using PhoneBookApp.Services.Report.Domain.Reports;

namespace PhoneBookApp.Services.Report.Application.Reports.GetReport;

internal sealed class GetReportByIdQueryHandler(IReportRepository reportRepository) : IQueryHandler<GetReportByIdQuery, GetReportDto>
{
    public async Task<Result<GetReportDto>> Handle(GetReportByIdQuery request, CancellationToken cancellationToken)
    {
        var report = await reportRepository.GetAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (report is null)
        {
            return Result.Failure<GetReportDto>(ReportErrors.NotFound(request.Id));
        }

        var reportDetails = await reportRepository.GetReportDetailListAsync(request.Id, 
            enableTracking: false,
            cancellationToken: cancellationToken);

        report.ReportDetails = reportDetails;

        var reportData = report.Adapt<GetReportDto>();

        return reportData;
    }
}
