using PhoneBookApp.Core.Application.Messaging;
using PhoneBookApp.Core.Domain;
using PhoneBookApp.Services.Contact.Domain.Reports;
using PhoneBookApp.Shared.Dtos;

namespace PhoneBookApp.Services.Contact.Application.Reports.GenerateReport;

internal sealed class GenerateReportQueryHandler(IGenerateReportRepository generateReportRepository) : IQueryHandler<GenerateReportQuery, List<ReportDetailDto>>
{
    public async Task<Result<List<ReportDetailDto>>> Handle(GenerateReportQuery request, CancellationToken cancellationToken)
    {
        List<ReportDetailDto> reports = await generateReportRepository.GetContactReports(cancellationToken);

        return reports;
    }
}