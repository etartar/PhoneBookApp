using Microsoft.EntityFrameworkCore;
using PhoneBookApp.Core.Application.Abstractions;
using PhoneBookApp.Core.Infrastructure.Extensions;
using PhoneBookApp.Services.Contact.Domain.Persons;

namespace PhoneBookApp.Services.Contact.Infrastructure.Database;

public sealed class ContactDbContext : DbContext, IUnitOfWork
{
    public ContactDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Person> Persons { get; set; }
    public DbSet<ContactInformation> ContactInformations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyGlobalFilters();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContactDbContext).Assembly);
    }
}
