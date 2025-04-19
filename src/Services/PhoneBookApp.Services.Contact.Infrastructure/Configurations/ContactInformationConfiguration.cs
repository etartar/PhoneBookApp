using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoneBookApp.Services.Contact.Domain.Persons;

namespace PhoneBookApp.Services.Contact.Infrastructure.Configurations;

internal sealed class ContactInformationConfiguration : IEntityTypeConfiguration<ContactInformation>
{
    public void Configure(EntityTypeBuilder<ContactInformation> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.PersonId);

        builder.Property(x => x.InformationType)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(x => x.InformationContent)
            .IsRequired()
            .HasMaxLength(255);

        builder.HasOne(x => x.Person)
            .WithMany(x => x.ContactInformations)
            .HasForeignKey(x => x.PersonId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
