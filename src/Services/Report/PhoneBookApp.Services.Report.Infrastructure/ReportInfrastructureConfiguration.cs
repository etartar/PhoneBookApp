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
using PhoneBookApp.Core.Infrastructure.Outbox;
using PhoneBookApp.Services.Report.Application;
using PhoneBookApp.Services.Report.Domain.Reports;
using PhoneBookApp.Services.Report.Infrastructure.Data;
using PhoneBookApp.Services.Report.Infrastructure.Database;
using PhoneBookApp.Services.Report.Infrastructure.Inbox;
using PhoneBookApp.Services.Report.Infrastructure.Outbox;
using PhoneBookApp.Services.Report.Infrastructure.Repositories;
using PhoneBookApp.Shared.IntegrationEvents;

namespace PhoneBookApp.Services.Report.Infrastructure;

public static class ReportInfrastructureConfiguration
{
    public static IServiceCollection AddReportInfrastructure(this IServiceCollection services, IConfiguration configuration, string databaseConnectionString)
    {
        services.AddDomainEventHandlers();

        services.AddIntegrationEventHandlers();

        NpgsqlDataSource npgsqlDataSource = new NpgsqlDataSourceBuilder(databaseConnectionString).Build();
        services.TryAddSingleton(npgsqlDataSource);

        services.TryAddScoped<IDbConnectionFactory, DbConnectionFactory>();

        services.AddDbContext<ReportDbContext>((sp, options) =>
            options
                .UseNpgsql(databaseConnectionString, npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "public"))
                .AddInterceptors(sp.GetRequiredService<InsertOutboxMessagesInterceptor>())
                .UseSnakeCaseNamingConvention());

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ReportDbContext>());

        services.AddScoped<IReportRepository, ReportRepository>();

        services.Configure<OutboxOptions>(configuration.GetSection("Outbox"));

        services.ConfigureOptions<ConfigureProcessOutboxJob>();

        services.Configure<InboxOptions>(configuration.GetSection("Inbox"));

        services.ConfigureOptions<ConfigureProcessInboxJob>();

        return services;
    }

    public static void ConfigureConsumers(IRegistrationConfigurator registrationConfigurator, string instanceId)
    {
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<ReportGeneratedIntegrationEvent>>()
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
