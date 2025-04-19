using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;

namespace PhoneBookApp.Services.Report.Infrastructure.Configurations;

internal sealed class ReportConfiguration : IEntityTypeConfiguration<Domain.Reports.Report>
{
    public void Configure(EntityTypeBuilder<Domain.Reports.Report> builder)
    {
        builder.ToCollection("reports");

        builder.HasKey(x => x.Id);

        builder.Property(p => p.Id)
            .HasColumnName("id");

        builder.Property(x => x.RequestDate).IsRequired()
            .HasColumnType("datetime");

        builder.Property(x => x.ReportStatus).IsRequired()
            .HasConversion<string>();

        builder.HasMany(x => x.ReportDetails)
            .WithOne()
            .HasForeignKey(x => x.ReportId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
