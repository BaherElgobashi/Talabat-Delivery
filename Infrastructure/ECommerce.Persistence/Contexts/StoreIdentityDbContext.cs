
using ECommerce.Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistence.Contexts
{
    public class StoreIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public StoreIdentityDbContext(DbContextOptions<StoreIdentityDbContext> options) : base(options)
        {
            
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Address>()
                   .ToTable("Addressess");

            builder.Entity<ApplicationUser>()
                   .ToTable("Users");

            builder.Entity<IdentityRole>()
                   .ToTable("Roles");

            builder.Entity<IdentityUserRole<string>>()
                   .ToTable("UserRoles");


            #region Ignored Tables.

            builder.Ignore<IdentityUserClaim<string>>();
            builder.Ignore<IdentityRoleClaim<string>>();
            builder.Ignore<IdentityUserLogin<string>>();
            builder.Ignore<IdentityUserToken<string>>();



            #endregion



        }


    }
}
