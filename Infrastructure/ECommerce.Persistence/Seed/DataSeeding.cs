using ECommerce.Domain.Contratcs.Seed;
using ECommerce.Domain.Models.Products;
using ECommerce.Persistence.Contexts;
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

        public DataSeeding(StoreDbContext context)
        {
            this.context = context;
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
    }
}
