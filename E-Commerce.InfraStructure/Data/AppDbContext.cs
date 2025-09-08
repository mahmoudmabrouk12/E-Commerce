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

       public  DbSet<Product>   Products {  get; set; }
        public DbSet<Category>  Categories { get; set; }
        public DbSet<Photo>      Photos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }





    }
}
