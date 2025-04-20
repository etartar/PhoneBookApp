using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PhoneBookApp.Core.Application.Behaviors;

namespace PhoneBookApp.Services.Contact.Application;

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

        return services;
    }
}
