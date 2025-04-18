using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using PhoneBookApp.Services.Contact.Infrastructure.Database;

namespace PhoneBookApp.Services.Contact.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string databaseConnectionString)
    {
        services.AddDbContext<ContactDbContext>((sp, options) =>
            options
                .UseNpgsql(databaseConnectionString, npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "public"))
                //.AddInterceptors(sp.GetRequiredService<UpdateAuditableInterceptor>())
                //.AddInterceptors(sp.GetRequiredService<InsertOutboxMessagesInterceptor>())/
                .UseSnakeCaseNamingConvention());

        return services;
    }
}
