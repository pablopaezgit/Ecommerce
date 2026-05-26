using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ECommerce.Domain.Entities;

namespace ECommerce.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
               .ValueGeneratedNever();

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(p => p.Description)
               .HasMaxLength(2000);

        builder.Property(p => p.Price)
               .IsRequired()
               .HasColumnType("decimal(18,2)");

        builder.Property(p => p.Stock)
               .IsRequired();

        builder.Property(p => p.CategoryId)
               .IsRequired();

        builder.Property(p => p.CreatedAt)
               .IsRequired();

        builder.HasIndex(p => p.Name);
    }
}