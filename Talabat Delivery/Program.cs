
using ECommerce.Abstraction.IServices;
using ECommerce.Domain.Contratcs.Repos;
using ECommerce.Domain.Contratcs.Seed;
using ECommerce.Domain.Contratcs.UOW;
using ECommerce.Domain.Models.Identity;
using ECommerce.Persistence.Contexts;
using ECommerce.Persistence.Repos;
using ECommerce.Persistence.Seed;
using ECommerce.Persistence.UOW;
using ECommerce.Service.MappingProfiles;
using ECommerce.Service.Services;
using ECommerce.Shared.ErrorModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System.Text;
using Talabat_Delivery.Custom_Exception_Middleware;

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
            //builder.Services.AddSwaggerGen();
           

            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOi...\""
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            Array.Empty<string>()
                        }
                    });
                            });

            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });

            builder.Services.AddScoped<IDataSeeding , DataSeeding>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddAutoMapper(m => m.AddProfile(new ProjectProfile(builder.Configuration)));

            builder.Services.AddScoped<IServiceManager,ServiceManager>();
            builder.Services.AddScoped<ICacheRepository , CacheRepository>();
            builder.Services.AddScoped<ICacheService , CacheService>();

            builder.Services.Configure<ApiBehaviorOptions>((options) =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var Errors = context.ModelState.Where(M => M.Value.Errors.Any())
                                                   .Select(M => new ValidationError()
                                                   {
                                                       Fields = M.Key,
                                                       Errors = M.Value.Errors.Select(e => e.ErrorMessage)
                                                   });
                    var Response = new ValidationErrorToReturn()
                    {
                        ValidationErrors = Errors
                    };

                    return new BadRequestObjectResult(Response);
                };
            });

            builder.Services.AddScoped<IBasketRepository , BasketRepository>();
            builder.Services.AddSingleton<IConnectionMultiplexer>((_) =>
            {
                return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection"));
            });

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<StoreIdentityDbContext>();

            builder.Services.AddAuthentication( config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWTOptions:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWTOptions:Audience"],
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            builder.Configuration["JWTOptions:SecurityKey"]
                            ?? throw new InvalidOperationException("JWT SecurityKey is not configured")
                        )
                    )

                };
            });


            #region CORS.

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", options =>
                {
                    options.AllowAnyHeader().AllowAnyMethod().WithOrigins(builder.Configuration["FrontendUrl"]);
                });
            });


            #endregion


            var app = builder.Build();

            // Seed the database with initial data
            await SeedDatabase(app);

            app.UseMiddleware<CustomExceptionMiddleWare>();

            #region Data Seeding.

            //var Scope = app.Services.CreateScope();

            //var ObjectSeeding = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();

            //await ObjectSeeding.DataSeedAsync(); 

            #endregion


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            
            

            app.MapControllers();

            app.Run();
        }

        public static async Task SeedDatabase(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var dbInitializer = services.GetRequiredService<IDataSeeding>();
                await dbInitializer.DataSeedAsync();
                await dbInitializer.IdentityInitializeAsync();
            }
            catch(Exception ex)
            {
                throw;
            }
            
        }
    }
}
