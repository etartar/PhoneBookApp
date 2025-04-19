using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PhoneBookApp.Core.Application.Abstractions;
using PhoneBookApp.Core.Application.Clock;
using PhoneBookApp.Core.Infrastructure.Clock;
using PhoneBookApp.Core.Infrastructure.Interceptors;
using PhoneBookApp.Services.Contact.Domain.Persons;
using PhoneBookApp.Services.Contact.Infrastructure.Database;
using PhoneBookApp.Services.Contact.Infrastructure.Repositories;

namespace PhoneBookApp.Services.Contact.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string databaseConnectionString)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.TryAddSingleton<UpdateSoftDeletableInterceptorInterceptor>();

        services.AddDbContext<ContactDbContext>((sp, options) =>
            options
                .UseNpgsql(databaseConnectionString, npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "public"))
                .AddInterceptors(sp.GetRequiredService<UpdateSoftDeletableInterceptorInterceptor>())
        //.AddInterceptors(sp.GetRequiredService<InsertOutboxMessagesInterceptor>())/
                .UseSnakeCaseNamingConvention());

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ContactDbContext>());

        services.AddScoped<IPersonRepository, PersonRepository>();

        return services;
    }
}
