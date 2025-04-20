using PhoneBookApp.Shared.Dtos;

namespace PhoneBookApp.Services.Contact.Domain.Reports;

public interface IGenerateReportRepository
{
    Task<List<ReportDetailDto>> GetContactReports(CancellationToken cancellationToken = default);
}
