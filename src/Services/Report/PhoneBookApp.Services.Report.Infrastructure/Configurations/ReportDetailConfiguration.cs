using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using PhoneBookApp.Services.Report.Domain.ReportDetails;

namespace PhoneBookApp.Services.Report.Infrastructure.Configurations;

internal sealed class ReportDetailConfiguration : IEntityTypeConfiguration<ReportDetail>
{
    public void Configure(EntityTypeBuilder<ReportDetail> builder)
    {
        builder.ToCollection("report_details");

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.ReportId);

        builder.Property(p => p.Id)
            .HasColumnName("id");

        builder.Property(p => p.ReportId)
            .IsRequired();

        builder.Property(x => x.Location)
            .IsRequired();

        builder.Property(x => x.TotalPersonCount)
            .IsRequired();

        builder.Property(x => x.TotalPhoneNumberCount)
            .IsRequired();

        builder.HasOne(x => x.Report)
            .WithMany()
            .HasForeignKey(x => x.ReportId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
