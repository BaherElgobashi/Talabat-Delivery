using ECommerce.Domain.Entities.Products;
using ECommerce.Presistance.Configurations.Products_Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presistance.DbContexts
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions <StoreDbContext> options) :base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // We use them because DbContext and Configurations in the same Project.
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); 

            // we use this if congiguration and DbContext not in the same project.
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly); // Both of them are the same 
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
    }
}
