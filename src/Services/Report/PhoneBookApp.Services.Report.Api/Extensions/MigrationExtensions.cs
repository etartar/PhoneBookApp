using Microsoft.EntityFrameworkCore;
using PhoneBookApp.Services.Report.Infrastructure.Database;

namespace PhoneBookApp.Services.Report.Api.Extensions;

public static class MigrationExtensions
{
    public static async Task ApplyMigrationsAsync(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using (ReportDbContext context = scope.ServiceProvider.GetRequiredService<ReportDbContext>())
        {
            await context.Database.MigrateAsync();
        }
    }
}
