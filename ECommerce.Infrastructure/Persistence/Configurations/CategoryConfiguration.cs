using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
 
namespace ECommerce.Infrastructure.Persistence.Configurations;
 
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");
 
        builder.HasKey(c => c.Id);
 
        builder.Property(c => c.Id)
               .ValueGeneratedNever(); // El Id lo genera el dominio (Guid.NewGuid)
 
        builder.Property(c => c.Name)
               .IsRequired()
               .HasMaxLength(100);
 
        builder.Property(c => c.Description)
               .HasMaxLength(500);
 
        // Dos categorías no pueden tener el mismo nombre
        builder.HasIndex(c => c.Name)
               .IsUnique();
    }
}
 