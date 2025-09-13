using E_Commerce.Core.Entites.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.InfraStructure.Data.Configuration
{
    public class OrdersConfiguration : IEntityTypeConfiguration<Orders>
    {
        public void Configure(EntityTypeBuilder<Orders> builder)
        {
          

          builder.OwnsOne(x => x.shippingAddress, x => { x.WithOwner(); });
          builder.HasMany(o=>o.orderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
          builder.Property(l=>l.Status)
          .HasConversion(l=>l.ToString() , l=>(Status)Enum.Parse(typeof(Status), l));

            builder.Property(p => p.SubTotal).HasColumnType("decimal(18,2)");

        }
    }
}
