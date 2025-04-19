using Microsoft.EntityFrameworkCore;
using PhoneBookApp.Core.Application.Abstractions;
using PhoneBookApp.Services.Report.Domain.ReportRequests;

namespace PhoneBookApp.Services.Report.Infrastructure.Database;

public class ReportDbContext : DbContext, IUnitOfWork
{
    public ReportDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<ReportRequest> ReportRequests { get; set; }
    public DbSet<Domain.Reports.Report> Reports { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReportDbContext).Assembly);
    }
}
