using E_Commerce.Core.Entites.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.InfraStructure.Data.Configuration
{
    public class DelivaryMethodConfiguration : IEntityTypeConfiguration<DelivaryMethod>
    {
        public void Configure(EntityTypeBuilder<DelivaryMethod> builder)
        {
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            builder.HasData( new DelivaryMethod 
            {
                       Id = 1,
                       Name = "Standard Delivery",
                       Description = "Delivers in 5-7 business days",
                       DelivaryTime = "5-7 days",
                       Price = 50m

            } );
        }
    }
}
