
using Ecommerce.web.Extensions;
using ECommerce.Domain.Contracts;
using ECommerce.Presistance.Data.DataSeed;
using ECommerce.Presistance.DbContexts;
using ECommerce.Presistance.Repositories;
using ECommerce.Services;
using ECommerce.Services.Abstraction.Services;
using ECommerce.Services.MappingProfiles;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace TalabatDelivery
{
    public class Program
    {
        public static async Task Main(string[] args)
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

            builder.Services.AddScoped<IUnitOfWork , UnitOfWork>();

            //builder.Services.AddAutoMapper(X => X.AddProfile<ProductProfile>());

            //builder.Services.AddAutoMapper(X=> X.LicenseKey = "" , typeof(ProductProfile).Assembly);

            builder.Services.AddAutoMapper(typeof(ServicesAssemblyReference).Assembly);
            

            builder.Services.AddScoped<IProductService , ProductService>();

            builder.Services.AddSingleton<IConnectionMultiplexer>(SP =>
            {
                return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection")!);
            }
            );


            

            #endregion

            var app = builder.Build();

            #region Data Seed.

            await app.MigrateDatabaseAsync();
            await app.SeedDatabaseAsync();

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

            app.UseStaticFiles();

            app.UseAuthorization();


            app.MapControllers();

            #endregion

            await app.RunAsync();
        }
    }
}
