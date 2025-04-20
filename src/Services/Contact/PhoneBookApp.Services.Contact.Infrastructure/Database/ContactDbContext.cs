using Microsoft.EntityFrameworkCore;
using PhoneBookApp.Core.Application.Abstractions;
using PhoneBookApp.Core.Infrastructure.Configurations;
using PhoneBookApp.Core.Infrastructure.Extensions;
using PhoneBookApp.Core.Infrastructure.Inbox;
using PhoneBookApp.Core.Infrastructure.Outbox;
using PhoneBookApp.Services.Contact.Domain.Persons;

namespace PhoneBookApp.Services.Contact.Infrastructure.Database;

public sealed class ContactDbContext : DbContext, IUnitOfWork
{
    public ContactDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Person> Persons { get; set; }
    public DbSet<ContactInformation> ContactInformations { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }
    public DbSet<OutboxMessageConsumer> OutboxMessageConsumers { get; set; }
    public DbSet<InboxMessage> InboxMessages { get; set; }
    public DbSet<InboxMessageConsumer> InboxMessageConsumers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyGlobalFilters();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContactDbContext).Assembly);

        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());

        modelBuilder.ApplyConfiguration(new OutboxMessageConsumerConfiguration());

        modelBuilder.ApplyConfiguration(new InboxMessageConfiguration());

        modelBuilder.ApplyConfiguration(new InboxMessageConsumerConfiguration());
    }
}
