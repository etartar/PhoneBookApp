using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using PhoneBookApp.Services.Report.Domain.ReportRequests;

namespace PhoneBookApp.Services.Report.Infrastructure.Configurations;

internal sealed class ReportRequestConfiguration : IEntityTypeConfiguration<ReportRequest>
{
    public void Configure(EntityTypeBuilder<ReportRequest> builder)
    {
        builder.ToCollection("report_requests");

        builder.HasKey(x => x.Id);

        builder.Property(p => p.Id)
            .HasColumnName("id");

        builder.Property(x => x.RequestDate).IsRequired()
            .HasColumnName("request_date")
            .HasColumnType("datetime");

        builder.Property(x => x.ReportStatus).IsRequired()
            .HasColumnName("report_status")
            .HasConversion<string>();

        builder.HasMany(x => x.Reports)
            .WithOne()
            .HasForeignKey(x => x.ReportRequestId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
