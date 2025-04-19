using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PhoneBookApp.Core.Application.Abstractions;
using PhoneBookApp.Core.Application.Clock;
using PhoneBookApp.Core.Infrastructure.Clock;
using PhoneBookApp.Services.Report.Domain.Reports;
using PhoneBookApp.Services.Report.Infrastructure.Database;
using PhoneBookApp.Services.Report.Infrastructure.Repositories;

namespace PhoneBookApp.Services.Report.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string databaseConnectionString)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddDbContext<ReportDbContext>(options =>
            options
                .UseMongoDB(databaseConnectionString, "ReportDB"));

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ReportDbContext>());

        services.AddScoped<IReportRepository, ReportRepository>();

        return services;
    }
}
