using PhoneBookApp.Core.Domain;
using PhoneBookApp.Services.Report.Domain.ReportDetails;

namespace PhoneBookApp.Services.Report.Domain.Reports;

public interface IReportRepository : IRepository<Report>
{
    Task<List<ReportDetail>> GetReportDetailListAsync(
        Guid reportId,
        Func<IQueryable<ReportDetail>, IOrderedQueryable<ReportDetail>>? orderBy = null,
        bool enableTracking = true,
        CancellationToken cancellationToken = default);

    Task CreateReportDetail(ReportDetail reportDetail, CancellationToken cancellationToken = default);
    Task CreateReportDetails(List<ReportDetail> reportDetails, CancellationToken cancellationToken = default);
}
