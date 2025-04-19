using PhoneBookApp.Core.Infrastructure.Repositories;
using PhoneBookApp.Services.Report.Domain.ReportRequests;
using PhoneBookApp.Services.Report.Infrastructure.Database;

namespace PhoneBookApp.Services.Report.Infrastructure.Repositories;

internal sealed class ReportRequestRepository : RepositoryBase<ReportRequest>, IReportRequestRepository
{
    public ReportRequestRepository(ReportDbContext dbContext) : base(dbContext)
    {
    }
}
