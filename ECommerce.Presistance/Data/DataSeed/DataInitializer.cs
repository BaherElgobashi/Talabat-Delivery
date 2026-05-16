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
        public void Initialize()
        {
            var HasProducts = _dbContext.Products.Any();

            var HasBrands = _dbContext.ProductBrands.Any();

            var HasTypes = _dbContext.ProductTypes.Any();

            if (!HasBrands)
            {
                SeedDataFromJson<ProductBrand, int>("brands.json", _dbContext.ProductBrands);
            }

            if(!HasTypes)
            {
                SeedDataFromJson<ProductType, int>("types.json", _dbContext.ProductTypes);
            }

            _dbContext.SaveChanges();

            if (!HasProducts)
            {
                SeedDataFromJson<Product, int>("products.json" , _dbContext.Products);
            }

            _dbContext.SaveChanges();

        }

        private void SeedDataFromJson <T, TKey> (string fileName , DbSet<T> dbset) where T : BaseEntity<TKey>
        {
            var FilePath = @"..\ECommerce.Presistance\Data\DataSeed\JSON Files\" + fileName;

            if (!File.Exists(FilePath))
            {
                throw new FileNotFoundException($"File {fileName} is not Existed.");
            }

            try
            {
                using var dataStream = File.OpenRead(FilePath);

                var data = JsonSerializer.Deserialize<List<T>>(dataStream, new JsonSerializerOptions()
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
