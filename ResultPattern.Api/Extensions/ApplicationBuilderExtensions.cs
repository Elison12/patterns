using Microsoft.EntityFrameworkCore;
using ResultPattern.Api.Database;

namespace ResultPattern.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    internal static void ApplyMigrations(this IApplicationBuilder builder)
    {
        using var scope = builder.ApplicationServices.CreateScope();

        using var applicationDbContext =
            scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        applicationDbContext.Database.Migrate();
    }
}