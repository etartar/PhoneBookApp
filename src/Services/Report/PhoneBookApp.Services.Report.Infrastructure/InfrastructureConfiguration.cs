using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PhoneBookApp.Core.Application.Abstractions;
using PhoneBookApp.Services.Report.Domain.ReportRequests;
using PhoneBookApp.Services.Report.Infrastructure.Database;
using PhoneBookApp.Services.Report.Infrastructure.Repositories;

namespace PhoneBookApp.Services.Report.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string databaseConnectionString)
    {
        services.AddDbContext<ReportDbContext>(options =>
            options
                .UseMongoDB(databaseConnectionString, "ReportDB"));

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ReportDbContext>());

        services.AddScoped<IReportRequestRepository, ReportRequestRepository>();

        return services;
    }
}
