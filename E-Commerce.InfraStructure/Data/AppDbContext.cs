using E_Commerce.Core.Entites.Product;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
namespace E_Commerce.InfraStructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        DbSet<Product>   products {  get; set; }
        DbSet<Category>  categories { get; set; }
        DbSet<Photo>      photos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }





    }
}
