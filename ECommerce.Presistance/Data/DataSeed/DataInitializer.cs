using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Entities.Products;
using ECommerce.Presistance.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ECommerce.Presistance.Data.DataSeed
{
    public class DataInitializer : IDataInitializer
    {
        private readonly StoreDbContext _dbContext;

        public DataInitializer(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task InitializeAsync()
        {
            try
            {
                var HasProducts = await _dbContext.Products.AnyAsync();

                var HasBrands = await _dbContext.ProductBrands.AnyAsync();

                var HasTypes = await _dbContext.ProductTypes.AnyAsync();

                if (HasProducts && HasBrands && HasTypes) return;

                if (!HasBrands)
                {
                    SeedDataFromJson<ProductBrand, int>("brands.json", _dbContext.ProductBrands);
                }

                if (!HasTypes)
                {
                    SeedDataFromJson<ProductType, int>("types.json", _dbContext.ProductTypes);
                }

                _dbContext.SaveChanges();

                if (!HasProducts)
                {
                    SeedDataFromJson<Product, int>("products.json", _dbContext.Products);
                }

                _dbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Data Seeding is Failed : {ex}");
            }

        }

        private async Task SeedDataFromJsonAsync <T, TKey> (string fileName , DbSet<T> dbset) where T : BaseEntity<TKey>
        {
            var FilePath = @"..\ECommerce.Presistance\Data\DataSeed\JSON Files\" + fileName;

            if (!File.Exists(FilePath))
            {
                throw new FileNotFoundException($"File {fileName} is not Existed.");
            }

            try
            {
                using var dataStream = File.OpenRead(FilePath);

                var data = await JsonSerializer.DeserializeAsync<List<T>>(dataStream, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true,
                });

                if (data is not null) 
                {
                    dbset.AddRange(data);
                }


            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error While Reading JSON File {ex}");

                return;
            }
        }
    }
}
