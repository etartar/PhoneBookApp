using Microsoft.EntityFrameworkCore;
using PhoneBookApp.Core.Application.Abstractions;
using PhoneBookApp.Core.Infrastructure.Configurations;
using PhoneBookApp.Core.Infrastructure.Inbox;
using PhoneBookApp.Core.Infrastructure.Outbox;
using PhoneBookApp.Services.Report.Domain.ReportDetails;

namespace PhoneBookApp.Services.Report.Infrastructure.Database;

public class ReportDbContext : DbContext, IUnitOfWork
{
    public ReportDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Domain.Reports.Report> Reports { get; set; }
    public DbSet<ReportDetail> ReportDetails { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }
    public DbSet<OutboxMessageConsumer> OutboxMessageConsumers { get; set; }
    public DbSet<InboxMessage> InboxMessages { get; set; }
    public DbSet<InboxMessageConsumer> InboxMessageConsumers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReportDbContext).Assembly);

        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());

        modelBuilder.ApplyConfiguration(new OutboxMessageConsumerConfiguration());

        modelBuilder.ApplyConfiguration(new InboxMessageConfiguration());

        modelBuilder.ApplyConfiguration(new InboxMessageConsumerConfiguration());
    }
}
