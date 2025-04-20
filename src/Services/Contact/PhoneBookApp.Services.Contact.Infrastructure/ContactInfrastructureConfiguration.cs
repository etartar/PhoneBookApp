using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using PhoneBookApp.Core.Application.Abstractions;
using PhoneBookApp.Core.Application.Data;
using PhoneBookApp.Core.Application.Messaging;
using PhoneBookApp.Core.EventBus;
using PhoneBookApp.Core.Infrastructure.Interceptors;
using PhoneBookApp.Core.Infrastructure.Outbox;
using PhoneBookApp.Services.Contact.Application;
using PhoneBookApp.Services.Contact.Domain.Outbox;
using PhoneBookApp.Services.Contact.Domain.Persons;
using PhoneBookApp.Services.Contact.Domain.Reports;
using PhoneBookApp.Services.Contact.Infrastructure.Data;
using PhoneBookApp.Services.Contact.Infrastructure.Database;
using PhoneBookApp.Services.Contact.Infrastructure.Inbox;
using PhoneBookApp.Services.Contact.Infrastructure.Outbox;
using PhoneBookApp.Services.Contact.Infrastructure.Repositories;
using PhoneBookApp.Shared.IntegrationEvents;

namespace PhoneBookApp.Services.Contact.Infrastructure;

public static class ContactInfrastructureConfiguration
{
    public static IServiceCollection AddContactInfrastructure(this IServiceCollection services, IConfiguration configuration, string databaseConnectionString)
    {
        services.AddDomainEventHandlers();

        services.AddIntegrationEventHandlers();

        NpgsqlDataSource npgsqlDataSource = new NpgsqlDataSourceBuilder(databaseConnectionString).Build();
        services.TryAddSingleton(npgsqlDataSource);

        services.TryAddScoped<IDbConnectionFactory, DbConnectionFactory>();

        services.TryAddSingleton<UpdateSoftDeletableInterceptorInterceptor>();

        services.AddDbContext<ContactDbContext>((sp, options) =>
            options
                .UseNpgsql(databaseConnectionString, npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "public"))
                .AddInterceptors(sp.GetRequiredService<UpdateSoftDeletableInterceptorInterceptor>())
                .AddInterceptors(sp.GetRequiredService<InsertOutboxMessagesInterceptor>())
                .UseSnakeCaseNamingConvention());

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ContactDbContext>());

        services.AddScoped<IOutboxRepository, OutboxRepository>();

        services.AddScoped<IPersonRepository, PersonRepository>();

        services.AddScoped<IGenerateReportRepository, GenerateReportRepository>();

        services.Configure<OutboxOptions>(configuration.GetSection("Outbox"));

        services.ConfigureOptions<ConfigureProcessOutboxJob>();

        services.Configure<InboxOptions>(configuration.GetSection("Inbox"));

        services.ConfigureOptions<ConfigureProcessInboxJob>();

        return services;
    }

    public static void ConfigureConsumers(IRegistrationConfigurator registrationConfigurator, string instanceId)
    {
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<ReportCreatedIntegrationEvent>>()
            .Endpoint(c => c.InstanceId = instanceId);
    }

    private static void AddDomainEventHandlers(this IServiceCollection services)
    {
        Type[] domainEventHandlers = AssemblyReference.Assembly
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IDomainEventHandler)))
            .ToArray();

        foreach (Type domainEventHandler in domainEventHandlers)
        {
            services.TryAddScoped(domainEventHandler);

            Type domainEvent = domainEventHandler
                .GetInterfaces()
                .Single(i => i.IsGenericType)
                .GetGenericArguments()
                .Single();

            Type closedIdempotentHandler = typeof(IdempotentDomainEventHandler<>).MakeGenericType(domainEvent);

            services.Decorate(domainEventHandler, closedIdempotentHandler);
        }
    }

    private static void AddIntegrationEventHandlers(this IServiceCollection services)
    {
        Type[] integrationEventHandlers = AssemblyReference.Assembly
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IIntegrationEventHandler)))
            .ToArray();

        foreach (Type integrationEventHandler in integrationEventHandlers)
        {
            services.TryAddScoped(integrationEventHandler);

            Type integrationEvent = integrationEventHandler
                .GetInterfaces()
                .Single(i => i.IsGenericType)
                .GetGenericArguments()
                .Single();

            Type closedIdempotentHandler =
                typeof(IdempotentIntegrationEventHandler<>).MakeGenericType(integrationEvent);

            services.Decorate(integrationEventHandler, closedIdempotentHandler);
        }
    }
}
