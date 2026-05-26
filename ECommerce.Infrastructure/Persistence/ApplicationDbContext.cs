using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Persistence;

// ApplicationDbContext implementa IUnitOfWork porque EF Core ya
// tiene SaveChangesAsync internamente — no necesitamos otra clase.
public class ApplicationDbContext : DbContext, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Product>   Products   => Set<Product>();
    public DbSet<Order>     Orders     => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<User>      Users      => Set<User>();
    public DbSet<Category>  Categories => Set<Category>(); // ← nuevo

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Aplica automáticamente todas las clases IEntityTypeConfiguration
        // que estén en este mismo ensamblado (las de la carpeta Configurations/)
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
