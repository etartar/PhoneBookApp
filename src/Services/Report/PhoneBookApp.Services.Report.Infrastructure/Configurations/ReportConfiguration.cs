using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PhoneBookApp.Services.Report.Infrastructure.Configurations;

internal sealed class ReportConfiguration : IEntityTypeConfiguration<Domain.Reports.Report>
{
    public void Configure(EntityTypeBuilder<Domain.Reports.Report> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(p => p.Id);

        builder.Property(x => x.RequestDate).IsRequired();

        builder.Property(x => x.ReportStatus).IsRequired()
            .HasConversion<string>();

        builder.HasMany(x => x.ReportDetails)
            .WithOne(x => x.Report)
            .HasForeignKey(x => x.ReportId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
