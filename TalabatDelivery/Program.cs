
using ECommerce.Domain.Contracts;
using ECommerce.Presistance.Data.DataSeed;
using ECommerce.Presistance.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace TalabatDelivery
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add Services to the Container.

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IDataInitializer, DataInitializer>();

            #endregion

            var app = builder.Build();

            #region Data Seed.

            using var scope = app.Services.CreateScope();

            var dbContextService = scope.ServiceProvider.GetRequiredService<StoreDbContext>();

            if (dbContextService.Database.GetPendingMigrations().Any())
            {
                dbContextService.Database.Migrate();
            }

            var DataInitializerService = scope.ServiceProvider.GetRequiredService<IDataInitializer>();

            DataInitializerService.Initialize();

            #endregion

            #region Configure the HTTP request pipeline.

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/openapi/v1.json", "Talabat Delivery API v1");
                });


            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            #endregion

            app.Run();
        }
    }
}
