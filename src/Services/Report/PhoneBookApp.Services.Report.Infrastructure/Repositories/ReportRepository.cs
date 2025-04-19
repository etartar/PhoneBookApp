using Microsoft.EntityFrameworkCore;
using PhoneBookApp.Core.Infrastructure.Repositories;
using PhoneBookApp.Services.Report.Domain.ReportDetails;
using PhoneBookApp.Services.Report.Domain.Reports;
using PhoneBookApp.Services.Report.Infrastructure.Database;

namespace PhoneBookApp.Services.Report.Infrastructure.Repositories;

internal sealed class ReportRepository : RepositoryBase<Domain.Reports.Report>, IReportRepository
{
    public ReportRepository(ReportDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<ReportDetail>> GetReportDetailListAsync(
        Guid reportId,
        Func<IQueryable<ReportDetail>, IOrderedQueryable<ReportDetail>>? orderBy = null,
        bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {
        IQueryable<ReportDetail> queryable = DbContext.Set<ReportDetail>().AsQueryable();

        queryable = queryable.Where(x => x.ReportId == reportId);

        if (!enableTracking)
        {
            queryable = queryable.AsNoTracking();
        }

        if (orderBy is not null)
        {
            return await orderBy(queryable).ToListAsync(cancellationToken);
        }

        return await queryable.ToListAsync(cancellationToken);
    }

    public async Task CreateReportDetail(ReportDetail reportDetail, CancellationToken cancellationToken = default)
    {
        await DbContext.Set<ReportDetail>().AddAsync(reportDetail, cancellationToken);
    }

    public async Task CreateReportDetails(List<ReportDetail> reportDetails, CancellationToken cancellationToken = default)
    {
        await DbContext.Set<ReportDetail>().AddRangeAsync(reportDetails, cancellationToken);
    }
}
