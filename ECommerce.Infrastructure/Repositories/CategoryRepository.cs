using ECommerce.Application.Contracts.Persistence;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext ctx) : base(ctx) { }

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken ct = default)
        => await _set.AnyAsync(c => c.Name.ToLower() == name.ToLower(), ct);
}