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
    }
}
