using FluentValidation;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using PhoneBookApp.Core.Application.Behaviors;
using PhoneBookApp.Services.Report.Application.Reports.GetReport;
using System.Reflection;

namespace PhoneBookApp.Services.Report.Application;

public static class ApplicationConfiguration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(ApplicationConfiguration).Assembly);

            config.AddOpenBehavior(typeof(ExceptionHandlingPipelineBehavior<,>));
            config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
        });

        services.AddValidatorsFromAssembly(typeof(ApplicationConfiguration).Assembly, includeInternalTypes: true);

        MappingConfigs();

        return services;
    }

    private static void MappingConfigs()
    {
        GetReportMapping.GetReportQueryMapping();

        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
    }
}
