using ECommerce.Application.Contracts.Persistence;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext ctx) : base(ctx) { }

    public async Task<IEnumerable<Product>> GetByCategoryAsync(
        Guid categoryId, CancellationToken ct = default)
        => await _set
            .AsNoTracking()
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync(ct);

    public async Task<bool> HasStockAsync(
        Guid productId, int quantity, CancellationToken ct = default)
    {
        var product = await _set.FindAsync(new object[] { productId }, ct);
        return product is not null && product.Stock >= quantity;
    }
}