using E_Commerce.Core.Entites.Product;
using E_Commerce.Core.Entites.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
namespace E_Commerce.InfraStructure.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Product>   Products {  get; set; }
        public virtual DbSet<Category>  Categories { get; set; }
        public virtual DbSet<Photo>      Photos { get; set; }
        public virtual DbSet<Address>    Addresses { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }





    }
}
