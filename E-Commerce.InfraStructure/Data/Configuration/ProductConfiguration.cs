using E_Commerce.Core.Entites.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.InfraStructure.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(l => l.Name).IsRequired().HasMaxLength(maxLength: 100);
            builder.Property(l => l.Description).IsRequired().HasMaxLength(maxLength: 100);
            builder.Property( l => l.NewPrice).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property( propertyExpression: l => l.OldPrice).IsRequired().HasColumnType("decimal(18,2)");
            builder.HasData(new Product
            {
                Id = 1,
                Name = "iPhone 15",
                Description = "Latest Apple smartphone with A17 chip",
                NewPrice = 1200,
                CategoryId = 1 
            },
            new Product
            {
            Id = 2,
            Name = "Samsung TV 55\"",
            Description = "Smart 4K Ultra HD LED TV",
            NewPrice = 800,
            CategoryId = 1 
            },
           new Product
           {
           Id = 3,
           Name = "Toyota Corolla",
           Description = "Reliable sedan with advanced safety features",
           NewPrice = 20000,
           CategoryId = 2 
           },
          new Product
          {
           Id = 4,
           Name = "Leather Jacket",
           Description = "Stylish black leather jacket",
           NewPrice = 150,
           CategoryId = 1
          });





        }
    }
}
