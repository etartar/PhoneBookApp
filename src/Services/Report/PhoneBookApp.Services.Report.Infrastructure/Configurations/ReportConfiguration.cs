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

        builder.HasIndex(x => x.ReportRequestId);

        builder.Property(p => p.Id)
            .HasColumnName("id");
        
        builder.Property(p => p.ReportRequestId)
            .IsRequired()
            .HasColumnName("report_request_id");

        builder.Property(x => x.Location)
            .IsRequired()
            .HasColumnName("location");
        
        builder.Property(x => x.TotalPersonCount)
            .IsRequired()
            .HasColumnName("total_person_count");
        
        builder.Property(x => x.TotalPhoneNumberCount)
            .IsRequired()
            .HasColumnName("total_phone_number_count");

        builder.HasOne(x => x.ReportRequest)
            .WithMany()
            .HasForeignKey(x => x.ReportRequestId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
