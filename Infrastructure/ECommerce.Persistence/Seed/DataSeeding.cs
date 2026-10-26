using ECommerce.Domain.Contratcs.Seed;
using ECommerce.Domain.Models.Identity;
using ECommerce.Domain.Models.Products;
using ECommerce.Persistence.Contexts;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Persistence.Seed
{
    public class DataSeeding : IDataSeeding
    {
        private readonly StoreDbContext context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly StoreIdentityDbContext _storeIdentityDbContext;

        public DataSeeding
        (StoreDbContext context , UserManager<ApplicationUser> userManager , RoleManager<IdentityRole> roleManager , StoreIdentityDbContext storeIdentityDbContext)
        {
            this.context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _storeIdentityDbContext = storeIdentityDbContext;
        }
        public async Task DataSeedAsync()
        {
            var PendingMigrations = await context.Database.GetPendingMigrationsAsync();
            if (PendingMigrations.Any())
                context.Database.Migrate();

            if (!context.ProductBrands.Any())
            {
                var ProductBrandData = await File.ReadAllTextAsync(@"..\Infrastructure\ECommerce.Persistence\Data\brands.json");

                var ProductBrands = JsonSerializer.Deserialize<List<ProductBrand>>(ProductBrandData);

                if(ProductBrands is not null && ProductBrands.Any())
                {
                    context.ProductBrands.AddRange(ProductBrands);
                }
            }


            if (!context.ProductTypes.Any())
            {
                var ProductTypeData = await File.ReadAllTextAsync(@"..\Infrastructure\ECommerce.Persistence\Data\types.json");

                var ProductTypes = JsonSerializer.Deserialize<List<ProductType>>(ProductTypeData);

                if (ProductTypes is not null && ProductTypes.Any())
                {
                    context.ProductTypes.AddRange(ProductTypes);
                }
            }

            if (!context.Products.Any())
            {
                var ProductsData = await File.ReadAllTextAsync(@"..\Infrastructure\ECommerce.Persistence\Data\products.json");

                var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);

                if(Products is not null && Products.Any())
                {
                    context.Products.AddRange(Products);
                }
            }

            



            context.SaveChanges();
        }

        public async Task IdentityInitializeAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }

                if (!_userManager.Users.Any())
                {
                    var User1 = new ApplicationUser()
                    {
                        Email = "Baher@gmail.com",
                        DisplayName = "Baher Mohamed",
                        PhoneNumber = "01113411677",
                        UserName = "B.02"
                    };
                    var User2 = new ApplicationUser()
                    {
                        Email = "Asser@gmail.com",
                        DisplayName = "Asser Mohamed",
                        PhoneNumber = "01556564970",
                        UserName = "NinjaMZ"
                    };


                    await _userManager.CreateAsync(User1);
                    await _userManager.CreateAsync(User2);

                    await _userManager.AddToRoleAsync(User1, "Admin");
                    await _userManager.AddToRoleAsync(User2, "SuperAdmin");



                }


                await _storeIdentityDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }


        }
    }
}
