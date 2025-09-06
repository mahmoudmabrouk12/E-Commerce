using E_Commerce.Core.Entites.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.InfraStructure.Data.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(l => l.Name).IsRequired().HasMaxLength(maxLength: 100);
            builder.Property(l => l.Id).IsRequired();
            builder.HasData(new Category { Id = 1, Name = "Electronics", Description = "Electronic devices and gadgets" },
                                 new Category { Id = 2, Name = "Cars", Description = "Vehicles and automobiles" },
                                 new Category { Id = 3, Name = "Clothing", Description = "Fashion and apparel" });

        }
    }
}
