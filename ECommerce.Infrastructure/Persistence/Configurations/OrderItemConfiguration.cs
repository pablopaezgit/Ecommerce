using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
 
namespace ECommerce.Infrastructure.Persistence.Configurations;
 
public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");
 
        builder.HasKey(i => i.Id);
 
        builder.Property(i => i.Id)
               .ValueGeneratedNever();
 
        builder.Property(i => i.UnitPrice)
               .IsRequired()
               .HasColumnType("decimal(18,2)");
 
        builder.Property(i => i.Quantity)
               .IsRequired();
 
        // Subtotal es una propiedad calculada (UnitPrice * Quantity),
        // no se persiste en la base de datos
        builder.Ignore(i => i.Subtotal);
    }
}
 