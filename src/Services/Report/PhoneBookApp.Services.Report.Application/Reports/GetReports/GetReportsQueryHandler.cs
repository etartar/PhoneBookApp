using Mapster;
using PhoneBookApp.Core.Application.Messaging;
using PhoneBookApp.Core.Domain;
using PhoneBookApp.Services.Report.Domain.Reports;

namespace PhoneBookApp.Services.Report.Application.Reports.GetReports;

internal sealed class GetReportsQueryHandler(IReportRepository reportRepository) : IQueryHandler<GetReportsQuery, List<GetReportsDto>>
{
    public async Task<Result<List<GetReportsDto>>> Handle(GetReportsQuery request, CancellationToken cancellationToken)
    {
        var reports = await reportRepository.GetListAsync(
            orderBy: x => x.OrderByDescending(r => r.RequestDate),
            cancellationToken: cancellationToken);

        return reports.Adapt<List<GetReportsDto>>();
    }
}
