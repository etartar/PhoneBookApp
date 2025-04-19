using Microsoft.EntityFrameworkCore;
using PhoneBookApp.Services.Contact.Infrastructure.Database;

namespace PhoneBookApp.Services.Contact.Api.Extensions;

public static class MigrationExtensions
{
    public static async Task ApplyMigrationsAsync(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using (ContactDbContext context = scope.ServiceProvider.GetRequiredService<ContactDbContext>())
        {
            await context.Database.MigrateAsync();
        }
    }
}
