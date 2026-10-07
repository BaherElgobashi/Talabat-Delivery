
using ECommerce.Abstraction.IServices;
using ECommerce.Domain.Contratcs.Seed;
using ECommerce.Domain.Contratcs.UOW;
using ECommerce.Persistence.Contexts;
using ECommerce.Persistence.Seed;
using ECommerce.Persistence.UOW;
using ECommerce.Service.MappingProfiles;
using ECommerce.Service.Services;
using Microsoft.EntityFrameworkCore;

namespace Talabat_Delivery
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            //builder.Services.AddOpenApi();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IDataSeeding , DataSeeding>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddAutoMapper(m => m.AddProfile(new ProjectProfile()));

            builder.Services.AddScoped<IServiceManager,ServiceManager>();

            var app = builder.Build();

            #region Data Seeding.

            var Scope = app.Services.CreateScope();

            var ObjectSeeding = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();

            await ObjectSeeding.DataSeedAsync(); 

            #endregion


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles();


            app.MapControllers();

            app.Run();
        }
    }
}
