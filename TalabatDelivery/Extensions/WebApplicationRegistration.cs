using ECommerce.Domain.Contracts;
using ECommerce.Presistance.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.web.Extensions
{
    public static class WebApplicationRegistration
    {
        public static async Task<WebApplication> MigrateDatabaseAsync(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();

            var dbContextService = scope.ServiceProvider.GetRequiredService<StoreDbContext>();

            var pendingMigrations = await dbContextService.Database.GetPendingMigrationsAsync();

            if (pendingMigrations.Any())
            {
               await dbContextService.Database.MigrateAsync();
            }

            return app;

        }

        public static async Task<WebApplication> SeedDatabaseAsync(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();

            var DataInitializerService = scope.ServiceProvider.GetRequiredService<IDataInitializer>();

            await DataInitializerService.InitializeAsync();

            return app;

        }

    }
}
