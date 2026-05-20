using ECommerce.Domain.Contracts;
using ECommerce.Presistance.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.web.Extensions
{
    public static class WebApplicationRegistration
    {
        public static WebApplication MigrateDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var dbContextService = scope.ServiceProvider.GetRequiredService<StoreDbContext>();

            if (dbContextService.Database.GetPendingMigrations().Any())
            {
                dbContextService.Database.Migrate();
            }

            return app;

        }

        public static async Task<WebApplication> SeedDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var DataInitializerService = scope.ServiceProvider.GetRequiredService<IDataInitializer>();

            await DataInitializerService.InitializeAsync();

            return app;

        }

    }
}
