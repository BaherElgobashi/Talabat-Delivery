
using ECommerce.Domain.Contratcs.Seed;
using ECommerce.Persistence.Contexts;
using ECommerce.Persistence.Seed;
using Microsoft.EntityFrameworkCore;

namespace Talabat_Delivery
{
    public class Program
    {
        public static void Main(string[] args)
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

            var app = builder.Build();

            #region Data Seeding.

            var Scope = app.Services.CreateScope();

            var ObjectSeeding = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();

            ObjectSeeding.DataSeed(); 
            #endregion


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
